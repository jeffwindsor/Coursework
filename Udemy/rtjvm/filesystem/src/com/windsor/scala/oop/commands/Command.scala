package com.windsor.scala.oop.commands

import com.windsor.scala.oop.filesystem.State

trait Command {

  def apply(state:State): State
}
object Command{

  def from(input:String): Command =
    new UnknownCommand
}