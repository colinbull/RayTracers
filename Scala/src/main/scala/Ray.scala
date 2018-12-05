import scala.util.Random

class Ray(orig:Vector3, dir:Vector3) {
  val origin = orig
  val direction = dir

  def pointAtT(t:Double) = origin + (direction * t)

}

object Ray {

  val defaultColour = Vector3(0.5, 0.7, 1.0)



  def trace(ray:Ray, scene: Scene, depth:Int):Vector3 = {
    scene.tryHit(ray, 0.00001, Double.MaxValue) match {
      case Some(hit) if depth < 50 => {
        hit.Material.scatter(ray, hit) match {
          case Some(hit) => hit.Attenuation * (trace(hit.Scattered, scene, depth + 1))
          case None => Vector3.zero
        }
      }
      case Some(_) => Vector3.zero
      case _ => {
        val t = 0.5 * (ray.direction.unit.Y + 1)
        (Vector3.one * (1 - t)) + (defaultColour * t)
      }
    }
  }
}
