import scala.util.Random
import scala.math.sqrt

object RayTracer extends App {
  val width = 800
  val height = 400
  val samples = 100
  val file = new PpmFile("scala_raytracer.ppm", width, height, d => (d * 255.99).toInt)

  val rnd = new Random()
  val rngY = Range(height, 0 ,-1)
  val rngX = Range(0, width, 1)
  val rngSamples = Range(0, samples, 1)

  val scene = Scene.random()

  val camera = Camera(90, width.toDouble / height.toDouble)

  for (y <- rngY) {
    for (x <- rngX) {
      var col = Vector3.zero
      for(_ <- rngSamples) {
        val i = (x + rnd.nextDouble()) / width.toDouble
        val j = (y + rnd.nextDouble()) / height.toDouble
        val ray = camera.castRay(i, j)
        col = col + Ray.trace(ray, scene, 0)
      }
      col = (col / samples).map(sqrt)

      file.writeLine(col.X, col.Y, col.Z)
    }
    println("Row: " + y)
  }

  file.close()
}
