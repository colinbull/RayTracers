import scala.math._
import org.scalatest._
import org.scalactic._

class Vector3Tests extends FunSuite {
  implicit val floatEq = TolerantNumerics.tolerantFloatEquality(1e-4f)

  test("A one vector should all be one.") {
    val v = Vector3.one
    assert(v.X === 1f)
    assert(v.Y === 1f)
    assert(v.Z === 1f)
  }

  test("A zero vector should all be zero.") {
    val v = Vector3.zero
    assert(v.X === 0f)
    assert(v.Y === 0f)
    assert(v.Z === 0f)
  }

  test("Plus operator does element wise addition of two vectors.") {
    val v = Vector3.create(2,2,2) + Vector3.create(3,3,3)
    assert(v.X === 5f)
    assert(v.Y === 5f)
    assert(v.Z === 5f)
  }

  test("Plus operator does element wise addition of vector and float.") {
    val v =  Vector3.create(2,2,2) + 3f
    assert(v.X === 5f)
    assert(v.Y === 5f)
    assert(v.Z === 5f)
  }

  test("Minus operator does element wise addition of two vectors.") {
    val v = Vector3.create(3,3,3) - Vector3.create(2,2,2)
    assert(v.X === 1f)
    assert(v.Y === 1f)
    assert(v.Z === 1f)
  }

  test("Multiply operator does element wise addition of two vectors.") {
    val v = Vector3.create(2,2,2) * Vector3.create(3,3,3)
    assert(v.X === 6f)
    assert(v.Y === 6f)
    assert(v.Z === 6f)
  }

  test("Divide operator does element wise addition of two vectors.") {
    val v = Vector3.create(6,6,6) / Vector3.create(2,2,2)
    assert(v.X === 3f)
    assert(v.Y === 3f)
    assert(v.Z === 3f)
  }

  test("Can compute the length of a 2 vector.") {
    val v = Vector3.create(2,2,2)
    assert(v.length === sqrt(12f).toFloat)
  }

  test("Can compute the length^2 of a 2 vector.") {
    val v = Vector3.create(2,2,2)
    assert(v.squaredLength === 12f)
  }

  test("Can create a unit vector from a vector.") {
    val v = Vector3.create(3,3,3).unit
    assert(v.length === 1f)
  }

  test("Can compute the dot product of 2 vectors.") {
    val v = Vector3.dot(
      Vector3.create(1,2,3),
      Vector3.create(3,2,1)
    )

    assert(v === 10f)
  }

  test("Can compute the cross product of 2 vectors.") {
    val v = Vector3.cross(
              Vector3.create(1,2,3),
              Vector3.create(3,2,1)
            )

    assert(v.X === -4f)
    assert(v.Y === 8f)
    assert(v.Z === -4f)
  }
}
