import scala.collection.mutable.ArrayBuffer
import scala.util.Random


class Scene(surfaces:Iterable[Surface]) extends Surface {
  override def tryHit(ray: Ray, tMin: Double, tMax: Double) = {
    var closest = tMax
    var tempHit:Option[Hit] = None
    for(surface <- surfaces) {
      (surface.tryHit(ray, tMin, closest)) match {
        case Some(hit) => {
          closest = hit.T
          tempHit = Some(hit)
        }
        case None => { }
      }
    }

    tempHit
  }
}

object Scene {
  def apply(surfaces:Iterable[Surface]) = new Scene(surfaces)

  def random() = {
    val rnd = new Random()
    val objects = ArrayBuffer[Surface]()

    objects += Sphere(Vector3(0,-1000, 0), 1000, Diffuse(0.5,0.5, 0.5))

    var i = 1;
    for(a <- -11 to 11 by 1) {
      for(b <- -11 to 11 by 1) {
        val center = Vector3(a-0.9*rnd.nextDouble(), 0.2, b+0.9*rnd.nextDouble())
        val mat =
            if (rnd.nextDouble() > 0.5) {
              Metal(0.5 * (1 + rnd.nextDouble()),0.5 * ( 1 + rnd.nextDouble()),0.5 * (1 + rnd.nextDouble()), 0.5 * rnd.nextDouble())
            } else {
              Diffuse(rnd.nextDouble() * rnd.nextDouble(),rnd.nextDouble() * rnd.nextDouble(),rnd.nextDouble() * rnd.nextDouble())
            }

        objects += Sphere(center, 0.2, mat)
      }
    }

    Scene(objects)
  }
}
