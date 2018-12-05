namespace StructRcdsNoMethods

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
     member __.AddLine(r:float, g:float, b:float) = 
        writer.WriteLine(sprintf "%d %d %d" (valueMap r) (valueMap g) (valueMap b))
            
     interface IDisposable with 
        member __.Dispose() = 
            writer.Flush()
            writer.Dispose()
            fileStream.Dispose()

module Utils = 
    let nextDouble = 
        let rand = new Random()
        rand.NextDouble

[<Struct>]
type Vector3 =
    { x:float; y:float; z:float } 


module Vector3Operators =   
    let (+.) (a:Vector3) (b:Vector3) = 
        { x = a.x + b.x; y = a.y + b.y; z = a.z + b.z }
        
    let (^+) (a:float) (b:Vector3) = 
        { x = a + b.x; y = a + b.y; z = a + b.z }
        
    let (+^) a (b:float) = 
        { x = a.x + b; y = a.y + b; z = a.z + b }
        
    let (-.) (a:Vector3) (b:Vector3) = 
        { x = a.x - b.x; y = a.y - b.y; z = a.z - b.z }
        
    let (^-) (a:float) (b:Vector3) = 
        { x = a - b.x; y = a - b.y; z = a - b.z }
    
    let (-^) a (b:float) = 
         { x = a.x - b; y = a.y - b; z = a.z - b }
        
    let ( *.) (a:Vector3) (b:Vector3) = 
        { x = a.x * b.x; y = a.y * b.y; z = a.z * b.z }
        
    let ( ^*) (a:float) (b:Vector3) = 
        { x = a * b.x; y = a * b.y; z = a * b.z }
    
    let ( *^) a (b:float) = 
        { x = a.x * b; y = a.y * b; z = a.z * b }
        
    let (/.) (a:Vector3) (b:Vector3) = 
        { x = a.x / b.x; y = a.y / b.y; z = a.z / b.z }
        
    let (^/) (a:float) (b:Vector3) = 
        { x = a / b.x; y = a / b.y; z = a / b.z }
    
    let (/^) a (b:float) = 
        { x = a.x / b; y = a.y / b; z = a.z / b }
        
[<RequireQualifiedAccess>]
module Vector3 = 

    open Vector3Operators

    let Zero = 
        { x = 0.; y = 0.; z = 0. }
    
    let One = 
        { x = 1.; y = 1.; z = 1. }

    let Create(x,y,z) = 
        { x = x; y = y; z = z } 

    let All(v) = 
        { x = v; y = v; z = v }
    
    let elementMap f (x:Vector3) = 
        { x = f x.x; y = f x.y; z = f x.z }
    
    let length (x:Vector3) = 
        sqrt(x.x*x.x + x.y*x.y + x.z*x.z) 
    
    let lengthSquared (x:Vector3) = 
        x.x*x.x + x.y*x.y + x.z*x.z
        
    let unit (x:Vector3) = 
        let k = 1. / (length x)
        { x = x.x * k; y = x.y * k; z = x.z * k }

    let dotProduct (a:Vector3, b:Vector3) = 
        a.x*b.x + a.y*b.y + a.z*b.z
        
    let crossProduct (a:Vector3, b:Vector3) =
        { x = a.y*b.z - a.z*a.y
          y = -(a.x*b.z - a.z*b.x)
          z = a.x*b.y - a.y*b.x }  
                  
    let reflect(a:Vector3, b:Vector3) = 
        a -. ((2. * dotProduct(a,b)) ^* b) 
         
open Vector3Operators

type Ray(origin:Vector3, direction:Vector3) = 
    
    member x.Origin = origin 
    member x.Direction = direction
    
    member x.AtPoint(t:float) = 
        origin +. (t ^* direction)
  
type MaterialHit =
     { attenuation:Vector3; scatteredRay:Ray } 

type RayHit =
     { t: float; p:Vector3; normal:Vector3; material: Material } 

and [<AbstractClass>] Material() = 
   
    abstract Scatter : ray:Ray * hit:RayHit -> Option<MaterialHit>
    abstract RandomUnitInSphere : unit -> Vector3
    default x.RandomUnitInSphere() =
        let newP() = Vector3.All(2. * Utils.nextDouble() - 1.)
        let mutable p = newP()
        while Vector3.lengthSquared p >= 1. do 
            p <- newP()
        p
     
type Surface = 
     abstract IsHit : ray:Ray * tMin:float * tMax:float -> Option<RayHit>
     
type Camera(origin, lowerLeftCorner, x, y) =

     new (verticalFieldOfView, aspect) =
        let theta = verticalFieldOfView * (Math.PI/180.)
        let halfHeight = Math.Tan(theta/2.)
        let halfWidth = aspect * halfHeight
        new Camera(
          Vector3.Create(0., 0.5, 0.),
          Vector3.Create(-halfWidth, -halfHeight, -1.0),
          Vector3.Create(2. * halfWidth, 0., 0.),
          Vector3.Create(0., 2. * halfHeight, 0.)
        )
          
     static member Default = 
        new Camera(Vector3.Zero, Vector3.Create(4., 0., 0.), Vector3.Create(0., 2., 0.),  Vector3.Create(-2., -1., -1.))
     


     member c.Ray(u:float, v:float) = 
         Ray(origin, lowerLeftCorner +. (u ^* x) +. (v ^* y) -. origin)
     
     
module Shapes = 
    let sphere(centre:Vector3, radius:float, mat:Material) = 
        {new Surface with
            member x.IsHit (ray,tMin,tMax) =
                let oc = ray.Origin -. centre 
                let a = Vector3.dotProduct(ray.Direction, ray.Direction)
                let b = 2.0 * Vector3.dotProduct(oc, ray.Direction)
                let c = Vector3.dotProduct(oc,oc) - (radius * radius)
                let discriminant = b*b - 4.0*a*c  
                if discriminant > 0. 
                then  
                    let temp = (-b - sqrt(discriminant)) / (2.0 * a)  
                    if (temp < tMax) && (temp > tMin) 
                    then  
                        let p = ray.AtPoint(temp)
                        Some { 
                            t = temp 
                            p = p
                            normal = (p -. centre) /^ radius 
                            material = mat 
                        }
                    else 
                        let temp = (-b + sqrt(discriminant)) / (2.0 * a) 
                        if (temp < tMax) && (temp > tMin) 
                        then 
                            let p = ray.AtPoint(temp)
                            Some { 
                                t = temp 
                                p = p
                                normal = (p -. centre) /^ radius 
                                material = mat 
                            }
                        else None    
                else  None             
        }

module Materials = 
    let metal (albedo:Vector3, fuzz) =
        { new Material() with 
            member x.Scatter(ray:Ray, hit:RayHit) = 
                let reflected = Vector3.reflect(Vector3.unit ray.Direction, hit.normal) +. (x.RandomUnitInSphere() *^ fuzz)
                let scattered = Ray(hit.p, reflected)
                let length = Vector3.dotProduct(scattered.Direction, hit.normal)
                if length > 0. 
                then Some { attenuation = albedo; scatteredRay = scattered }
                else None
        } 

    let diffuse (albedo:Vector3) =          
        { new Material() with 
            member x.Scatter(ray:Ray, hit:RayHit) = 
                let reflected = hit.p +. hit.normal +. x.RandomUnitInSphere()
                let scattered = Ray(hit.p, reflected -. hit.p)
                Some { attenuation = albedo; scatteredRay = scattered }
        }             

type Scene(items:seq<Surface>) =

    static member Random(seed) = 
        let rnd = new Random(seed)
        let objects = ResizeArray<Surface>()

        objects.Add(Shapes.sphere (Vector3.Create(0.,-1000., 0.), 1000., Materials.diffuse(Vector3.Create(0.5,0.5, 0.5))))
        let rng = [-11 .. 1 .. 11]
        let mutable i = 1;
        for a in  rng do
          for b in rng do
            let a = float a 
            let b = float b
            let center = Vector3.Create(a-0.9*rnd.NextDouble(), 0.2, b+0.9*rnd.NextDouble())
            let mat =
                if (rnd.NextDouble() > 0.5)
                then Materials.metal (Vector3.Create(0.5 * (1. + rnd.NextDouble()),0.5 * ( 1. + rnd.NextDouble()),0.5 * (1. + rnd.NextDouble())), 0.5 * rnd.NextDouble())
                else Materials.diffuse (Vector3.Create(rnd.NextDouble() * rnd.NextDouble(),rnd.NextDouble() * rnd.NextDouble(),rnd.NextDouble() * rnd.NextDouble()))
                

            objects.Add(Shapes.sphere (center, 0.2, mat))

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
        match surface.IsHit(ray, 0.001, Double.MaxValue) with 
        | Some hit -> 
            if depth < 50
            then 
                match hit.material.Scatter(ray, hit) with 
                | Some m -> m.attenuation *. (trace(m.scatteredRay, surface, depth + 1))
                | None -> Vector3.Zero
            else Vector3.Zero              
        | _ -> 
            let t = 0.5 * ((Vector3.unit ray.Direction).y + 1.)
            (Vector3.All(1. - t)) +. (Vector3.Create(0.5 * t, 0.7 * t, 1.0 * t))
              

    member x.Render(camera:Camera, scene) = 
        use file = new PPMFile(file, width, height, fun v -> (int (255.99 * v)))
        
        let rngY = [|file.Y .. -1 .. 1|]
        let rngX = [|0 .. 1 .. (file.X - 1)|] 
        let samples = [|0 .. (samples - 1)|]
   
        for y in rngY do 
            for x in rngX do
                let mutable col = Vector3.Zero
                for s in samples do 
                    let i = ((float x) + Utils.nextDouble()) / (float file.X)
                    let j = ((float y) + Utils.nextDouble()) / (float file.Y)
                    let ray = camera.Ray(i,j)
                    col <- col +. trace(ray, scene, 0) 
                
                col <- col /^ (float samples.Length)   
                col <- Vector3.elementMap sqrt col
                file.AddLine(col.x,col.y,col.z)

module Executor = 

    let run(seed, width, height, samples) =
        let scene = Scene.Random(seed)
        let tracer = new RayTracer("output_struct_rcds_nomethods.ppm", width, height, samples)  
        tracer.Render(Camera(90., 2.), scene)


            
