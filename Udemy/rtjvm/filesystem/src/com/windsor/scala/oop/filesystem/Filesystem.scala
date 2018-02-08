package com.windsor.scala.oop.filesystem

import java.util.Scanner

import com.windsor.scala.oop.commands.Command
import com.windsor.scala.oop.files.Directory

object Filesystem extends App {

  val root = Directory.ROOT
  var state = State(root,root)
  val scanner = new Scanner(System.in)

  // Command Loop
  while(true){
    state.show
    val input = scanner.nextLine
    state = Command.from(input).apply(state)
  }
}
