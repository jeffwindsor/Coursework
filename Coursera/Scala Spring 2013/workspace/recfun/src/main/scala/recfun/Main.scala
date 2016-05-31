package recfun
import common._

object Main {
  def main(args: Array[String]) {
    println("Pascal's Triangle")
    for (row <- 0 to 10) {
      for (col <- 0 to row)
        print(pascal(col, row) + " ")
      println()
    }
  }

  def factorial(n:Int):Int = 
    if (n==0) 1 else n * factorial(n-1)
  /**
   * Exercise 1
   */
  def pascal(c: Int, r: Int): Int = 
    (factorial(r) / factorial(r-c))/factorial(c)

  /**
   * Exercise 2
   */
  def balance(chars: List[Char]): Boolean = {
      def inner(cs: List[Char], b: Int): Int =
        if(cs.isEmpty || b < 0) b
        else if(cs.head == '(') inner(cs.tail, b + 1)
        else if (cs.head == ')') inner(cs.tail, b - 1)
        else inner(cs.tail, b)
        
      inner(chars, 0) == 0
    }

  /**
   * Exercise 3
   */
  def countChange(money: Int, coins: List[Int]): Int = ???
}
