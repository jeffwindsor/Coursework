import java.util.Arrays;
import java.util.ArrayList;

public class Fast
{
    public static void main(String[] args) {           
        Point[] points = Brute.readPointFile(args[0]);
        Brute.resetDrawArea();
        
        //Cycle through points
        int N = points.length;
        int last = N - 1;
        ArrayList<Point> segment = new ArrayList<Point>();
        
        //N-3 because must have at least 4 points for line
        for (int i = 0; i < N - 3; i++) {
            segment.add(points[i]);
                
            //Sort remaining points against origin
            int first = i + 1;
            Arrays.sort(points, first, last, points[i].SLOPE_ORDER);           
            for (int j = first; j <= last; j++){
             
                if (segment.size() == 1 || segment.get(0).slopeTo(segment.get(1)) == segment.get(0).slopeTo(points[j])){
                    //Add current point as first in segment
                    segment.add(points[j]);
                } else {
                    //Emit segment if has 4 or more points
                    if (segment.size() > 3)
                    {
                        //Draw line first to last
                        Point[] sorted = segment.toArray(new Point[segment.size()]);
                        Arrays.sort(sorted);
                        sorted[0].drawTo(sorted[sorted.length - 1]);
                                
                        //Print Points small to Large
                        StdOut.println(Brute.formatSegment(sorted));
                    }
                    //Reset segment slope
                    segment.clear();
                    segment.add(points[i]);
                }
            }
        }
    }
   

}