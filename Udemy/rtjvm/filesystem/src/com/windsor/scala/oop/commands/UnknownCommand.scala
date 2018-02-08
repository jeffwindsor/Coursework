package com.windsor.scala.oop.commands

import com.windsor.scala.oop.filesystem.State

class UnknownCommand extends Command {

  override def apply(state:State): State =
    state.setMessage("Command not found!")
}
