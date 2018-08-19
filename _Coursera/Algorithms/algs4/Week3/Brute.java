import java.util.Arrays;

public class Brute
{
    public static void main(String[] args) {           
        Point[] points = readPointFile(args[0]);
        resetDrawArea();
        
        //Cycle through points
        int N = points.length;
        double ab, ac, ad;
        Point pa,pb,pc,pd;
        for (int a = 0; a < N; a++) {
          
            //Draw point
            pa = points[a];
            pa.draw();
            
            for (int b = a + 1; b < N; b++) {
                pb = points[b];
                for (int c = b + 1; c < N; c++) {
                    pc = points[c];
                    //If slope of first three match then check forth points
                    if (pa.SLOPE_ORDER.compare(pb,pc) == 0) {                       
                        for (int d = c + 1; d < N; d++) {
                            pd = points[d];
                            if (pa.SLOPE_ORDER.compare(pc,pd) == 0){
                                //Sort Points
                                Point[] segment = new Point[] {pa,pb,pc,pd};
                                Arrays.sort((Object[])segment);
                                
                                //Draw line first to last
                                segment[0].drawTo(segment[3]);
                                
                                //Print Points small to Large
                                StdOut.println(formatSegment(segment));
                            }   
                        }
                    }
                }
            }
        }
    }
    
    //would like to enforce this to 4
    private static String ARROW = " -> ";
    public static String formatSegment(Point[] segment){
        String result = segment[0].toString();
        for (int i = 1; i < segment.length; i++){
            result = result + ARROW + segment[i].toString();
        }
        return result;
    }
    
    public static void resetDrawArea(){
        StdDraw.setXscale(0, 32768);
        StdDraw.setYscale(0, 32768); 
    }
        
    public static Point[] readPointFile(String filename){
        
        In in = new In(filename);
        int N = in.readInt();
        
        Point[] result = new Point[N];
        for (int i = 0; i < N; i++) {
            int x = in.readInt();
            int y = in.readInt();
            result[i] = new Point(x, y);
        }
        return result;
    }
}