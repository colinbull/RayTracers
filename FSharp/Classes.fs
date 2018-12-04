namespace Classes 

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

type Vector3(e0:float, e1:float, e2:float) =

    member __.X = e0
    member __.Y = e1
    member __.Z = e2
    
    static member Zero = Vector3(0.,0.,0.)
    
    static member One = Vector3(1.,1.,1.)
        
    static member Create(v) = Vector3(v,v,v)
    
    member x.elementMap f = Vector3(f x.X, f x.Y, f x.Z)
    
    member x.Length = 
        sqrt(x.X*x.X + x.Y*x.Y + x.Z*x.Z) 
    
    member x.LengthSquared = 
        x.X*x.X + x.Y*x.Y + x.Z*x.Z
        
    member x.Unit = 
        let k = 1. / x.Length
        Vector3(x.X * k, x.Y * k, x.Z * k)
   
    static member (+) (a:Vector3, b:Vector3) = 
        Vector3(a.X + b.X,a.Y + b.Y, a.Z + b.Z)
        
    static member (+) (a:float, b:Vector3) = 
        Vector3(a + b.X, a + b.Y, a + b.Z)
        
    static member (+) (a:Vector3, b:float) = 
        Vector3(a.X + b, a.Y + b, a.Z + b)
        
    static member (-) (a:Vector3, b:Vector3) = 
        Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z)
        
    static member (-) (a:float, b:Vector3) = 
        Vector3(a - b.X, a - b.Y, a - b.Z)
    
    static member (-) (a:Vector3, b:float) = 
        Vector3(a.X - b, a.Y - b, a.Z - b)
        
    static member (*) (a:Vector3, b:Vector3) = 
        Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z)
        
    static member (*) (a:float, b:Vector3) = 
        Vector3(a * b.X, a * b.Y, a * b.Z)
    
    static member (*) (a:Vector3, b:float) = 
        Vector3(a.X * b, a.Y * b, a.Z * b)
        
    static member (/) (a:Vector3, b:Vector3) = 
        Vector3(a.X / b.X,a.Y / b.Y, a.Z / b.Z)
        
    static member (/) (a:float, b:Vector3) = 
        Vector3(a / b.X,a / b.Y, a / b.Z)
    
    static member (/) (a:Vector3, b:float) = 
        Vector3(a.X / b, a.Y / b, a.Z / b)
        
    static member dotProduct (a:Vector3, b:Vector3) = 
        a.X*b.X + a.Y*b.Y + a.Z*b.Z
        
    static member crossProduct (a:Vector3, b:Vector3) =
        Vector3(a.Y*b.Z - a.Z*a.Y,
                -(a.X*b.Z - a.Z*b.X),
                a.X*b.Y - a.Y*b.X)
                  
    static member reflect(a:Vector3, b:Vector3) = 
        a - 2. * Vector3.dotProduct(a,b) * b 
         
  
type Ray(origin:Vector3, direction:Vector3) = 
    
    member x.Origin = origin 
    member x.Direction = direction
    
    member x.AtPoint(t:float) = 
        origin + (t * direction)

type MaterialHit =
     { attenuation:Vector3; scatteredRay:Ray } 

type RayHit =
     { t: float; p:Vector3; normal:Vector3; material: Material } 

and [<AbstractClass>] Material() = 
    abstract Scatter : ray:Ray * hit:RayHit -> Option<MaterialHit>
    abstract RandomUnitInSphere : unit -> Vector3
    default x.RandomUnitInSphere() =
        let newP() = Vector3.Create(2. * Utils.nextDouble() - 1.)
        let mutable p = Vector3.Zero
        while p.LengthSquared >= 1. do 
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
          Vector3(0., 0.5, 0.),
          Vector3(-halfWidth, -halfHeight, -1.0),
          Vector3(2. * halfWidth, 0., 0.),
          Vector3(0., 2. * halfHeight, 0.)
        )
          
     static member Default = 
        new Camera(Vector3.Zero, Vector3(4., 0., 0.), Vector3(0., 2., 0.),  Vector3(-2., -1., -1.))
     


     member c.Ray(u:float, v:float) = 
         Ray(origin, lowerLeftCorner + u*x + v*y - origin)
     
     

type Sphere(centre:Vector3, radius:float, mat:Material) = 
     interface Surface with
        member x.IsHit (ray,tMin,tMax) =
            let oc = ray.Origin - centre 
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
                        normal = (p - centre) / radius 
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
                            normal = (p - centre) / radius 
                            material = mat 
                        }
                    else None    
            else  None             
    

type Metal(albedo:Vector3, fuzz) =
    inherit Material() 

    override x.Scatter(ray:Ray, hit:RayHit) = 
        let reflected = Vector3.reflect(ray.Direction.Unit, hit.normal) + (x.RandomUnitInSphere() * fuzz)
        let scattered = Ray(hit.p, reflected)
        let length = Vector3.dotProduct(scattered.Direction, hit.normal)
        if length > 0. 
        then Some { attenuation = albedo; scatteredRay = scattered }
        else None
    

type Diffuse(albedo:Vector3) =          
    inherit Material() 

    override x.Scatter(ray:Ray, hit:RayHit) = 
        let reflected = hit.p + hit.normal + x.RandomUnitInSphere()
        let scattered = Ray(hit.p, reflected-hit.p)
        Some { attenuation = albedo; scatteredRay = scattered }
                 

type Scene(items:seq<Surface>) =

    static member Random(seed) = 
        let rnd = new Random(seed)
        let objects = ResizeArray<Surface>()

        objects.Add(Sphere (Vector3(0.,-1000., 0.), 1000., Diffuse(Vector3(0.5,0.5, 0.5))))
        let rng = [-11 .. 1 .. 11]
        let mutable i = 1;
        for a in  rng do
          for b in rng do
            let a = float a 
            let b = float b
            let center = Vector3(a-0.9*rnd.NextDouble(), 0.2, b+0.9*rnd.NextDouble())
            let mat =
                if (rnd.NextDouble() > 0.5)
                then  Metal (Vector3(0.5 * (1. + rnd.NextDouble()),0.5 * ( 1. + rnd.NextDouble()),0.5 * (1. + rnd.NextDouble())), 0.5 * rnd.NextDouble()) :> Material 
                else  Diffuse (Vector3(rnd.NextDouble() * rnd.NextDouble(),rnd.NextDouble() * rnd.NextDouble(),rnd.NextDouble() * rnd.NextDouble())) :> Material
                

            objects.Add(Sphere (center, 0.2, mat))
          
        
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

    let defaultColor = Vector3(0.5, 0.7, 1.0)

    let rec trace(ray:Ray, surface:Surface, depth:int) =
        match surface.IsHit(ray, 0.001, Double.MaxValue) with 
        | Some hit -> 
            if depth < 50
            then 
                match hit.material.Scatter(ray, hit) with 
                | Some m -> m.attenuation * (trace(m.scatteredRay, surface, depth + 1))
                | None -> Vector3.Zero
            else Vector3.Zero              
        | _ -> 
            let t = 0.5 * (ray.Direction.Unit.Y + 1.)
            (Vector3.Create(1. - t)) + (Vector3(0.5 * t, 0.7 * t, 1.0 * t))
              

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
                    col <- col + trace(ray, scene, 0) 
                
                col <- col / (float samples.Length)   
                col <- col.elementMap sqrt
                file.AddLine(col.X,col.Y,col.Z)


module Executor = 

    let run(seed, width, height, samples) =
        let scene = Scene.Random(seed)
        let tracer = new RayTracer("output_classes.ppm", width, height, samples)  
        tracer.Render(Camera(90., 2.), scene)