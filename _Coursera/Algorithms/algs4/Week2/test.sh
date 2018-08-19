#!/bin/sh
checkstyle Deque.java RandomizedQueue.java
rm *.class
javac -classpath /home/jeff/Documents/Coursework/algs4/stdlib.jar:/home/jeff/Documents/Coursework/algs4/algs4.jar Deque.java RandomizedQueue.java
findbugs *.class



