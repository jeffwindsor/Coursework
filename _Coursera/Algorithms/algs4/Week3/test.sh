#!/bin/sh
#checkstyle Point.java Brute.java Fast.java
rm *.class
javac -classpath /home/jeff/Documents/Coursework/algs4/stdlib.jar:/home/jeff/Documents/Coursework/algs4/algs4.jar Point.java Brute.java  Fast.java PointPlotter.java
findbugs *.class



