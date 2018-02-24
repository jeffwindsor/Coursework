package test.poker

object Poker extends App {

  /*
   * Given a set of 5 playing card identifiers such as 2H, 7C, QS, 10D, 2D;
   * determine if this hand is better than some other hand, according to the rules of poker.
   *
   * Hands will be a string with 5 cards comma separated,
   * each card will have 1-2 digits or JQKA and a suit indicator C,D,S,H (i.e. 10C, KH)
   *
   * Possible Hand Types Below:
   *   Straight flush
   *   Four of a kind
   *   Full house
   *   Flush
   *   Straight
   *   Three of a kind
   *   Two pair
   *   One pair
   *
   * The goal of this is to compare between the hand types.
   * Comparing 2 of the same type (i.e. 2 straights) to determine a winner is outside the scope
   * and will not be tested.
   *
   * Implement hand1WinsOverHand2 method and return whether or not the first hand wins over the second hand.
   */
  type Hand = Seq[Card]
  case class Card(rank:Int, suit:Char)

  //Parsing of inputs
  def parseHand(handStr: String): Hand = handStr.split(",").map(parseCard)
  def parseCard(cardStr: String): Card = (cardStr: Seq[Char]) match {
    case Seq('A', suit) => Card(14,suit)
    case Seq('K', suit) => Card(13,suit)
    case Seq('Q', suit) => Card(12,suit)
    case Seq('J', suit) => Card(11,suit)
    case Seq('1', '0', suit) => Card(10,suit)
    case Seq(num, suit) => Card(num.asDigit,suit)
  }
  
  //Identify Hand and Score it
  def hasSameSuit(h:Hand):Boolean = h.forall(c => c.suit == h.head.suit)
  def hasSequentialRanks(h:Hand):Boolean = {
    val ranks = h.map(c => c.rank)
    //TODO - Not considering the loop around ace here
    (ranks.max) - (ranks.min) == (h.length - 1)
  }  
  def flushPoints(h:Hand):Int = if (hasSameSuit(h)) 15 else 0
  def straightPoints(h:Hand):Int = if (hasSequentialRanks(h)) 10 else 0
  def kindPoints(h:Hand):Int = {
    val kinds = h.groupBy(c => c.rank).values.filter(v => v.length > 1).groupBy(v => v.length)
    kinds.keys.fold(0) {(acc,k) => (scala.math.pow(k,2).toInt * kinds(k).toList.length) + acc}
  }
  def score(h:Hand): Int = flushPoints(h) + straightPoints(h) + kindPoints(h)

  //
  def hand1WinsOverHand2(hand1Str: String, hand2Str: String): Boolean =
    score(parseHand(hand1Str)) > score(parseHand(hand2Str))

  implicit class CompareTwoPokerHands(hand1: String) {
    def winsOver(hand2: String): Unit = {
      val result = if (hand1WinsOverHand2(hand1, hand2)) "Correct" else "Incorrect"
      println(s"$result, hand [$hand1] wins over [$hand2]")
    }
  }

  println("Poker Hand comparison")
  "8C,9C,10C,JC,QC" winsOver "6S,7H,8D,9H,10D" // straight flush    (15+10) = 25
  "4H,4D,4C,4S,JS" winsOver "6C,6S,KH,AS,AD" // four of a kind      (4^2) = 16
  "5C,3C,10C,KC,7C" winsOver "6C,6D,6H,9C,KD" // Flush              15
  "4H,4D,4C,KC,KD" winsOver "9D,6S,KH,AS,AD" // full house          (9+4) = 13
  "2C,3C,4S,5S,6S" winsOver "6C,6D,6H,9C,KD" // straights           10
  "7C,7D,7S,3H,4D" winsOver "9S,6S,10D,AS,AD" // three of a kind    (3^2) = 9
  "8C,8H,10S,KH,KS" winsOver "2S,2D,JH,7S,AC" // two pair           (4+4) = 8
  "AC,AH,3C,QH,10C" winsOver "3S,2D,KH,JS,AD" // one pair           2^2 = 4
  System.exit(0)

  /*
  println(parseCard("8C"))  //Card
  println(parseHand("2C,9C,10C,JC,AC"))  //Hand
  println("2C,9C,10C,JC,AC hasSameSuit:" + hasSameSuit(parseHand("2C,9C,10C,JC,AC")))
  println("2C,9C,10C,JH,AC hasSameSuit:" + hasSameSuit(parseHand("2C,9C,10C,JH,AC")))
  println("8C,9C,10C,JC,QC hasSequentialRanks:" + hasSequentialRanks(parseHand("8C,9C,10C,JC,QC")))
  println("2C,9C,10C,JH,AC hasSequentialRanks:" + hasSequentialRanks(parseHand("2C,9C,10C,JH,AC")))
  println("4H,4D,4C,4S,JS rank counts:" + groupByRanks(parseHand("4H,4D,4C,4S,JS")))
  println("4H,4D,3C,3S,JS rank counts:" + groupByRanks(parseHand("4H,4D,3C,3S,JS")))
  
  println(kindPoints(parseHand("8C,9C,10C,JC,QC"))) //0
  println(kindPoints(parseHand("4H,4D,4C,4S,JS" ))) //16
  println(kindPoints(parseHand("5C,3C,10C,KC,7C"))) //0
  println(kindPoints(parseHand("4H,4D,4C,KC,KD" ))) //9 + 4 = 13
  println(kindPoints(parseHand("2C,3C,4S,5S,6S" ))) //0
  println(kindPoints(parseHand("7C,7D,7S,3H,4D" ))) //9
  println(kindPoints(parseHand("8C,8H,10S,KH,KS"))) //4 + 4 = 8
  println(kindPoints(parseHand("AC,AH,3C,QH,10C"))) //4
  
  println(score(parseHand("8C,9C,10C,JC,QC")), score(parseHand("6S,7H,8D,9H,10D"))) // straight flush    (15+10) = 25
  println(score(parseHand("4H,4D,4C,4S,JS" )), score(parseHand( "6C,6S,KH,AS,AD"))) // four of a kind      (4^2) = 16
  println(score(parseHand("5C,3C,10C,KC,7C" )), score(parseHand( "6C,6D,6H,9C,KD"))) // Flush              15
  println(score(parseHand("4H,4D,4C,KC,KD" )), score(parseHand( "9D,6S,KH,AS,AD"))) // full house          (9+4) = 13
  println(score(parseHand("2C,3C,4S,5S,6S" )), score(parseHand( "6C,6D,6H,9C,KD"))) // straights           10
  println(score(parseHand("7C,7D,7S,3H,4D" )), score(parseHand( "9S,6S,10D,AS,AD"))) // three of a kind (3^2) = 9
  println(score(parseHand("8C,8H,10S,KH,KS" )), score(parseHand( "2S,2D,JH,7S,AC"))) // two pair      (4+4) = 8
  println(score(parseHand("AC,AH,3C,QH,10C" )), score(parseHand( "3S,2D,KH,JS,AD"))) // one pair      2^2 = 4
  */
}