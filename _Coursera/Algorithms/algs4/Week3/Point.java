/*************************************************************************
 * Name:  Jeff Windsor
 * Email: jeff.windsor@gmail.com
 *
 * Compilation:  javac Point.java
 * Execution:
 * Dependencies: StdDraw.java
 *
 * Description: An immutable data type for points in the plane.
 *
 *************************************************************************/

import java.util.Comparator;

public class Point implements Comparable<Point> {

    // compare points by slope
    public final Comparator<Point> SLOPE_ORDER = new BySlope(this);

    private final int x;                              // x coordinate
    private final int y;                              // y coordinate

    // create the point (x, y)
    public Point(int x, int y) {
        /* DO NOT MODIFY */
        this.x = x;
        this.y = y;
    }

    // plot this point to standard drawing
    public void draw() {
        /* DO NOT MODIFY */
        StdDraw.point(x, y);
    }

    // draw line between this point and that point to standard drawing
    public void drawTo(Point that) {
        /* DO NOT MODIFY */
        StdDraw.line(this.x, this.y, that.x, that.y);
    }

    // slope between this point and that point
    public double slopeTo(Point that) {
        //Degenerate Segment
        if (compareTo(that) == 0) return Double.POSITIVE_INFINITY;
        //Vertical Line Segment
        if (this.x == that.x) return Double.POSITIVE_INFINITY;
        //Horizontal Line Segment
        if (this.y == that.y) return 0.0;
        //Slope
        return (that.y - this.y) / (double)(that.x - this.x);
    }

    // is this point lexicographically smaller than that one?
    // comparing y-coordinates and breaking ties by x-coordinates
    public int compareTo(Point that) {
        int result = compare(y, that.y);
        if (result == 0 ) result =  compare(x, that.x);
        return result;
    }

    // return string representation of this point
    public String toString() {
        /* DO NOT MODIFY */
        return "(" + x + ", " + y + ")";
    }

    // unit test
    public static void main(String[] args) {
        /* YOUR CODE HERE */
    }
    
    private class BySlope implements Comparator<Point>
    {
        Point local;
        public BySlope(Point local)
        {
            this.local = local;
        }
        public int compare(Point a, Point b){
            return Point.compare(local.slopeTo(a), local.slopeTo(b));
        }
    }
    
    private static int compare(int x, int y){
        return (x > y ? +1 : x < y ? -1 : 0);
    }
    
    private static double DOUBLE_COMPARE_LIMIT = 0.000001;
    private static int compare(double x, double y){
        double delta = x - y;
        return (delta >= DOUBLE_COMPARE_LIMIT ? +1: delta <= DOUBLE_COMPARE_LIMIT ? -1 : 0);
    }
}