package com.windsor.scala.oop.files

class Directory(override val parentPath:String, override val name:String, val contents:List[DirEntry])
  extends DirEntry(parentPath,name) {

}

object Directory{
  val SEPERATOR = "/"
  val ROOT_PATH = "/"

  def ROOT = empty("", "")
  def empty(parentPath:String, name:String) =
    new Directory(parentPath, name, List())

}
