class Camera(origin:Vector3, lowerLeftCorner:Vector3, horiz:Vector3, vert:Vector3) {

  def castRay(x:Double, y:Double) = {
    new Ray(origin, lowerLeftCorner + (horiz * x) + (vert * y) - origin)
  }
}

object Camera {
  def default = new Camera(
      Vector3.zero,
      Vector3(-2, -1, -1),
      Vector3(4,0,0),
      Vector3(0, 2, 0)
  )

  def apply(verticalFieldOfView:Double, aspect:Double): Camera = {
    val theta = verticalFieldOfView * (Math.PI/180)
    val halfHeight = Math.tan(theta/2)
    val halfWidth = aspect * halfHeight
    new Camera(
      Vector3(0,0.5,0),
      Vector3(-halfWidth, -halfHeight, -1.0),
      Vector3(2*halfWidth, 0, 0),
      Vector3(0, 2*halfHeight, 0)
    )
  }
}
