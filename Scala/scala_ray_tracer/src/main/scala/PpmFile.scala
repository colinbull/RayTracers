import java.io.{File, PrintWriter}

class PpmFile(path:String, x:Int, y:Int, normalizer:Double => Int) {
  private val header = "P3\n" + x + " " + y + "\n255\n"
  private val writer = new PrintWriter(new File(path))

  writer.println(header)

  def writeLine(r:Double, g:Double, b:Double): Unit = {
    val ir = normalizer(r)
    val ig = normalizer(g)
    val ib = normalizer(b)
    val data = ir + " " + ig + " " + ib + "\n"
    writer.write(data)
  }

  def close(): Unit = {
    writer.flush()
    writer.close();
  }

}
