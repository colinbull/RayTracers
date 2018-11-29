import scala.util.Random

class Hit(t:Double, position:Vector3, normal:Vector3, material: Material) {
  val T = t
  val Position = position
  val Normal = normal
  val Material = material
}

trait Surface {
  def tryHit (ray:Ray, tMin:Double, tMax:Double) : Option[Hit]
}

class MaterialHit(hit:Hit, attenuation:Vector3, scattered:Ray) {
  val Hit = hit
  val Attenuation = attenuation
  val Scattered = scattered
}

trait Material {
  val rnd = new Random()

  def scatter(ray:Ray, hit:Hit) : Option[MaterialHit]

  protected def randomUnitVectorInSphere() = {
    var p = Vector3.zero
    do {
      p =  (Vector3(rnd.nextDouble()) * 2.0) - Vector3.one
    } while (p.squaredLength >= 1)

    p
  }
}

class Diffuse(albedo:Vector3) extends Material {
  val Albedo = albedo

  override def scatter(ray: Ray, hit:Hit): Option[MaterialHit] = {
    val target = hit.Position + hit.Normal + randomUnitVectorInSphere()
    val newRay = new Ray(hit.Position, target - hit.Position)
    Some(new MaterialHit(hit, albedo, newRay))
  }
}

object Diffuse {
  def apply(x:Double,y:Double, z:Double) = new Diffuse(Vector3(x,y,z))
}

class Metal(albedo:Vector3, fuzz:Double = 1) extends Material {
  override def scatter(ray: Ray, hit: Hit): Option[MaterialHit] = {
     val reflectedRay = Vector3.reflect(ray.direction.unit, hit.Normal) + (randomUnitVectorInSphere() * fuzz)
     val newRay = new Ray(hit.Position, reflectedRay)
     val length = Vector3.dot(newRay.direction, hit.Normal)
     if(length > 0) {
       Some(new MaterialHit(hit, albedo, newRay))
     } else {
       None
     }
  }
}

object Metal {
  def apply(x:Double,y:Double, z:Double, fuzz:Double = 1) = new Metal(Vector3(x,y,z), fuzz)
}