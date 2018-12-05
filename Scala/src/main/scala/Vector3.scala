import scala.math._
import scala.language.implicitConversions

class Vector3(e0:Double, e1:Double, e2:Double) {

  val X = e0
  val Y = e1
  val Z = e2

  def squaredLength= X*X + Y*Y + Z*Z

  def length = sqrt(squaredLength)

  def unit= {
      val k = 1.0f / length
      new Vector3(X * k, Y * k, Z * k)
  }

  def + (b:Vector3) = new Vector3(X + b.X, Y + b.Y, Z + b.Z)
  def / (b:Vector3) = new Vector3(X / b.X, Y / b.Y, Z / b.Z)
  def * (b:Vector3) = new Vector3(X * b.X, Y * b.Y, Z * b.Z)
  def - (b:Vector3) = new Vector3(X - b.X, Y - b.Y, Z - b.Z)
  def + (b:Double)  = new Vector3(X + b, Y + b, Z + b)
  def - (b:Double)  = new Vector3(X - b, Y - b, Z - b)
  def * (b:Double)  = new Vector3(X * b, Y * b, Z * b)
  def / (b:Double)  = new Vector3(X / b, Y / b, Z / b)

  def map(f:Double => Double) = new Vector3(f(X), f(Y), f(Z))

  override def toString: String = X + " " + Y + " " + Z
}

object Vector3 {
  def one: Vector3 = new Vector3(1,1,1)
  def zero: Vector3 = new Vector3(0,0,0)
  def apply(e0: Double, e1: Double, e2: Double): Vector3 = new Vector3(e0, e1, e2)
  def apply(e:Double) = new Vector3(e,e,e)

  def dot(a:Vector3, b:Vector3) = a.X * b.X + a.Y * b.Y + a.Z * b.Z

  def cross(a:Vector3, b:Vector3) =
    new Vector3(
      a.Y * b.Z - a.Z * b.Y,
      -(a.X * b.Z - a.Z * b.X),
      a.X * b.Y - a.Y * b.X
    )

  def reflect(incoming:Vector3, normal:Vector3) =
      incoming - (normal * (2.0 * dot(incoming, normal)))
}



