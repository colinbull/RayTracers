import scala.math.sqrt

//Eqn for a sphere (x-Cx)^2 + (y-Cy)^2 + (z-Cz)^2 = R^2
//generalizing to vector and a none origin centre
//(p - C) (from now on S) where p is the vector representing the position of the sphere
//taking since we want S^2 the dot product gives use this thus,
//dot(S,S) = R^2 to test see if we have a hit we have to compute the roots of this equation
//we will either have 0, 1 or 2 roots depending on where we are on the sphere.
//to do this we need to substitute the pointAtT (origin + t*dir) for a Ray and solve the equation.
//this give
//  (t^2)*dot(dir,dir) + 2*(t^2)*dot(dir, origin-C) + dot(origin-C,origin-C) - R^2 = 0
//since this is quadratic. We can solve using the quadratic formula, b^2 - 4ac since we are only
//interested in the case where we have 1 or 2 roots >= 0, 0 roots obviously means a miss at this point.
//
class Sphere(centre:Vector3, radius:Double, material:Material) extends Surface {
  override def tryHit(ray:Ray, tMin:Double, tMax:Double):Option[Hit] = {
    val originFromCenter = ray.origin - centre
    val a = Vector3.dot(ray.direction, ray.direction)
    val b = 2.0 * Vector3.dot(originFromCenter, ray.direction)
    val c = Vector3.dot(originFromCenter, originFromCenter) - (radius*radius)
    val discriminant = b*b - 4.0*a*c

    if(discriminant > 0) {
      var t = (-b - sqrt(discriminant)) / (2.0 * a)
      if(t < tMax && t > tMin) {
        val position = ray.pointAtT(t)
        val normal = (position - centre) / radius
        Some(new Hit(t, position, normal,material))
      } else {
        t = (-b + sqrt(discriminant)) / (2.0 * a)
        if (t < tMax && t > tMin) {
          val position = ray.pointAtT(t)
          val normal = (position - centre) / radius
          Some(new Hit(t, position, normal, material))
        } else { None }
      }
    } else { None }
  }
}

object Sphere {
  def apply(center:Vector3, radius:Double, material: Material) = new Sphere(center, radius, material)
}
