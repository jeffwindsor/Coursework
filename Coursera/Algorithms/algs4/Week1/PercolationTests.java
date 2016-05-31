public class PercolationTests {

    public static void main(String[] args) {
        PercolationStats ps = new PercolationStats(10,10);
        System.out.println("mean                    = " + ps.mean());
     System.out.println("stddev                  = " + ps.stddev());
     System.out.println("95% confidence interval = " 
                            + ps.confidenceLo() 
                            + ", " + ps.confidenceHi());
    }
    
    private static void TestIndex(Percolation p, int expected, int i, int j) {
//        int index = p.toIndex(i, j);
//        AssertAreEqual(expected,index);
    }
    private static void TestValid(Percolation p, int i, int j) {
//        boolean valid = p.validateCoordinate(i);
//        StdOut.println("Expected " +i + " True " + valid);
    }
     private static void AssertIsTrue(boolean actual) {
         if (!actual)
            StdOut.println("Expected True but was False");
     }
    private static void AssertAreEqual(int expected, int actual) {
        if (actual != expected)
            StdOut.println("Expected Index " + expected + " but was " + actual);
    }   
        
}