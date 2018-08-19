#!/bin/sh
checkstyle Percolation.java PercolationStats.java
javac Percolation.java PercolationStats.java
findbugs Percolation.class PercolationStats.class


