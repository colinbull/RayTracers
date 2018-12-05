namespace StructRecds32

open System
open System.IO 

type PPMFile(fileName:string, x, y, valueMap) = 
     
     let fileStream = File.Open(fileName, FileMode.Create, FileAccess.Write)
     let writer = new StreamWriter(fileStream)
     
     
     do 
        writer.WriteLine("P3")
        writer.WriteLine(sprintf "%d %d" x y) 
        writer.WriteLine("255")
     
     member __.X = x 
     member __.Y = y
     member __.AddLine(r:float32, g:float32, b:float32) = 
        writer.WriteLine(sprintf "%d %d %d" (valueMap r) (valueMap g) (valueMap b))
            
     interface IDisposable with 
        member __.Dispose() = 
            writer.Flush()
            writer.Dispose()
            fileStream.Dispose()

module Utils = 
    let nextDouble = 
        let rand = new Random()
        rand.NextDouble >> float32

[<Struct>]
type Vector3 =
    { x:float32; y:float32;  z:float32 } 
    
    static member Zero = 
        { x = 0.f; y = 0.f; z = 0.f }
    
    static member One = 
        { x = 1.f; y = 1.f; z = 1.f }
        
    static member Create(?x,?y,?z) = 
        { x = defaultArg x 0.f; y = defaultArg y 0.f; z = defaultArg z 0.f }

    static member Create(v) = 
        { x = v; y = v; z = v }
    
    member x.elementMap f = 
        { x = f x.x; y = f x.y; z = f x.z }
    
    member x.Length = 
        sqrt(x.x*x.x + x.y*x.y + x.z*x.z) 
    
    member x.LengthSquared = 
        x.x*x.x + x.y*x.y + x.z*x.z
        
    member x.Unit = 
        let k = 1.f / x.Length
        { x = x.x * k; y = x.y * k; z = x.z * k }
    
    static member (+) (a:Vector3, b:Vector3) = 
        { x = a.x + b.x; y = a.y + b.y; z = a.z + b.z }
        
    static member (+) (a:float32, b:Vector3) = 
        { x = a + b.x; y = a + b.y; z = a + b.z }
        
    static member (+) (a, b:float32) = 
        { x = a.x + b; y = a.y + b; z = a.z + b }
        
    static member (-) (a:Vector3, b:Vector3) = 
        { x = a.x - b.x; y = a.y - b.y; z = a.z - b.z }
        
    static member (-) (a:float32, b:Vector3) = 
        { x = a - b.x; y = a - b.y; z = a - b.z }
    
    static member (-) (a, b:float32) = 
        { x = a.x - b; y = a.y - b; z = a.z - b }
        
    static member (*) (a:Vector3, b:Vector3) = 
        { x = a.x * b.x; y = a.y * b.y; z = a.z * b.z }
        
    static member (*) (a:float32, b:Vector3) = 
        { x = a * b.x; y = a * b.y; z = a * b.z }
    
    static member (*) (a, b:float32) = 
        { x = a.x * b; y = a.y * b; z = a.z * b }
        
    static member (/) (a:Vector3, b:Vector3) = 
        { x = a.x / b.x; y = a.y / b.y; z = a.z / b.z }
        
    static member (/) (a:float32, b:Vector3) = 
        { x = a / b.x; y = a / b.y; z = a / b.z }
    
    static member (/) (a, b:float32) = 
        { x = a.x / b; y = a.y / b; z = a.z / b }
        
    static member dotProduct (a:Vector3, b:Vector3) = 
        a.x*b.x + a.y*b.y + a.z*b.z
        
    static member crossProduct (a:Vector3, b:Vector3) =
        { x = a.y*b.z - a.z*a.y
          y = -(a.x*b.z - a.z*b.x)
          z = a.x*b.y - a.y*b.x }  
                  
    static member reflect(a:Vector3, b:Vector3) = 
        a - 2.f * Vector3.dotProduct(a,b) * b 
         
  
type Ray(origin:Vector3, direction:Vector3) = 
    
    member x.Origin = origin 
    member x.Direction = direction
    
    member x.AtPoint(t:float32) = 
        origin + (t * direction)

type MaterialHit =
     { attenuation:Vector3; scatteredRay:Ray } 

type RayHit =
     { t: float32; p:Vector3; normal:Vector3; material: Material } 

and [<AbstractClass>] Material() = 
   
    abstract Scatter : ray:Ray * hit:RayHit -> Option<MaterialHit>
    abstract RandomUnitInSphere : unit -> Vector3
    default x.RandomUnitInSphere() =
        let newP() = Vector3.Create(2.f * Utils.nextDouble() - 1.f)
        let mutable p = newP()
        while p.LengthSquared >= 1.f do 
            p <- newP()
        p
     
type Surface = 
     abstract IsHit : ray:Ray * tMin:float32 * tMax:float32 -> Option<RayHit>
     
type Camera(origin, lowerLeftCorner, x, y) =

     new (verticalFieldOfView, aspect) =
        let theta = verticalFieldOfView * (Math.PI/180.)
        let halfHeight = float32(Math.Tan(theta / 2.))
        let halfWidth = aspect * halfHeight
        new Camera(
          Vector3.Create(0.f, 0.5f, 0.f),
          Vector3.Create(-halfWidth, -halfHeight, -1.0f),
          Vector3.Create(2.f * halfWidth, 0.f, 0.f),
          Vector3.Create(0.f, 2.f * halfHeight, 0.f)
        )
          
     static member Default = 
        new Camera(Vector3.Zero, Vector3.Create(x = 4.f), Vector3.Create(y = 2.f),  Vector3.Create(-2.f, -1.f, -1.f))
     


     member c.Ray(u:float32, v:float32) = 
         Ray(origin, lowerLeftCorner + u*x + v*y - origin)
     
     
module Shapes = 
    let sphere(centre:Vector3, radius:float32, mat:Material) = 
        {new Surface with
            member x.IsHit (ray,tMin,tMax) =
                let oc = ray.Origin - centre 
                let a = Vector3.dotProduct(ray.Direction, ray.Direction)
                let b = 2.0f * Vector3.dotProduct(oc, ray.Direction)
                let c = Vector3.dotProduct(oc,oc) - (radius * radius)
                let discriminant = b*b - 4.0f*a*c  
                if discriminant > 0.f
                then  
                    let temp = (-b - sqrt(discriminant)) / (2.0f * a)  
                    if (temp < tMax) && (temp > tMin) 
                    then  
                        let p = ray.AtPoint(temp)
                        Some { 
                            t = temp 
                            p = p
                            normal = (p - centre) / radius 
                            material = mat 
                        }
                    else 
                        let temp = (-b + sqrt(discriminant)) / (2.0f * a) 
                        if (temp < tMax) && (temp > tMin) 
                        then 
                            let p = ray.AtPoint(temp)
                            Some { 
                                t = temp 
                                p = p
                                normal = (p - centre) / radius 
                                material = mat 
                            }
                        else None    
                else  None             
        }

module Materials = 
    let metal (albedo:Vector3, fuzz) =
        { new Material() with 
            member x.Scatter(ray:Ray, hit:RayHit) = 
                let reflected = Vector3.reflect(ray.Direction.Unit, hit.normal) + (x.RandomUnitInSphere() * fuzz)
                let scattered = Ray(hit.p, reflected)
                let length = Vector3.dotProduct(scattered.Direction, hit.normal)
                if length > 0.f 
                then Some { attenuation = albedo; scatteredRay = scattered }
                else None
        } 

    let diffuse (albedo:Vector3) =          
        { new Material() with 
            member x.Scatter(ray:Ray, hit:RayHit) = 
                let reflected = hit.p + hit.normal + x.RandomUnitInSphere()
                let scattered = Ray(hit.p, reflected-hit.p)
                Some { attenuation = albedo; scatteredRay = scattered }
        }             

type Scene(items:seq<Surface>) =

    static member Random(seed) = 
        let rnd = 
            let r = new Random(seed)
            r.NextDouble >> float32

        let objects = ResizeArray<Surface>()

        objects.Add(Shapes.sphere (Vector3.Create(0.f,-1000.f, 0.f), 1000.f, Materials.diffuse(Vector3.Create(0.5f,0.5f, 0.5f))))
        let rng = [-11 .. 1 .. 11]
        let mutable i = 1;
        for a in  rng do
          for b in rng do
            let a = float32 a 
            let b = float32 b
            let center = Vector3.Create(a-0.9f*rnd(), 0.2f, b+0.9f*rnd())
            let mat =
                if (rnd() > 0.5f)
                then Materials.metal (Vector3.Create(0.5f * (1.f + rnd()),0.5f * ( 1.f + rnd()),0.5f * (1.f + rnd())), 0.5f * rnd())
                else Materials.diffuse (Vector3.Create(rnd() * rnd(),rnd() * rnd(),rnd() * rnd()))
                

            objects.Add(Shapes.sphere (center, 0.2f, mat))
          
        
        Scene(objects)


    interface Surface with
        member x.IsHit(ray, tMin, tMax) = 
            let mutable hasHit = None 
            let mutable closest = tMax 
            for item in items do 
                match item.IsHit(ray, tMin, closest) with 
                | Some hit -> 
                    hasHit <- Some hit
                    closest <- hit.t 
                | None -> () 
            hasHit

type RayTracer(file, width, height, samples) = 

    let rec trace(ray:Ray, surface:Surface, depth:int) =
        match surface.IsHit(ray, 0.001f, Single.MaxValue) with 
        | Some hit -> 
            if depth < 50
            then 
                match hit.material.Scatter(ray, hit) with 
                | Some m -> m.attenuation * (trace(m.scatteredRay, surface, depth + 1))
                | None -> Vector3.Zero
            else Vector3.Zero              
        | _ -> 
            let t = 0.5f * (ray.Direction.Unit.y + 1.f)
            (Vector3.Create(1.f - t)) + (Vector3.Create(0.5f * t, 0.7f * t, 1.0f * t))
              

    member x.Render(camera:Camera, scene) = 
        let rand = new Random()
        use file = new PPMFile(file, width, height, fun v -> (int (255.99f * v)))
        
        let rngY = [|file.Y .. -1 .. 1|]
        let rngX = [|0 .. 1 .. (file.X - 1)|] 
        let samples = [|0 .. (samples - 1)|]
   
        for y in rngY do 
            for x in rngX do
                let mutable col = Vector3.Zero
                for s in samples do 
                    let i = ((float32 x) + Utils.nextDouble()) / (float32 file.X)
                    let j = ((float32 y) + Utils.nextDouble()) / (float32 file.Y)
                    let ray = camera.Ray(i,j)
                    col <- col + trace(ray, scene, 0) 
                
                col <- col / (float32 samples.Length)   
                col <- col.elementMap sqrt
                file.AddLine(col.x,col.y,col.z)


module Executor = 

    let run(seed, width, height, samples) =
        let scene = Scene.Random(seed)
        let tracer = new RayTracer("output_struct_rcds32.ppm", width, height, samples)  
        tracer.Render(Camera(90., 2.f), scene)


            
