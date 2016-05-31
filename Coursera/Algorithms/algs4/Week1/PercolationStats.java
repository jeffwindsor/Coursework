public class PercolationStats {
   private double[] results;

   public PercolationStats(int N, int T) {
     //Validate input
     if (N <= 0 || T <= 0)
       throw new IllegalArgumentException();
     
     //Init
     results = new double[T];
     int totalSites = N * N;
     
     //Run trials
     for (int t = 0; t < T; t++) {
       int count = 0;
       Percolation p = new Percolation(N);
       //Open sites until precolated
       while (!p.percolates()) {
         //Open random coordinates
         int i = StdRandom.uniform(N) + 1;
         int j = StdRandom.uniform(N) + 1;
         if (!p.isOpen(i, j)) {
           count++;
           p.open(i, j);
         }         
       }
       results[t] = (count / (double)totalSites);
       System.out.println(t + ") " + count + " / " + totalSites + " = " + results[t]);
     }
   }
   
   public double mean() { 
       return StdStats.mean(results);
   }
   
   public double stddev() { 
       return StdStats.stddev(results);
   }
   
   public double confidenceLo() { 
       return mean() - (1.96d * stddev() / (Math.sqrt((double) results.length)));
   }
   
   public double confidenceHi() { 
       return mean() + (1.96d * stddev() / (Math.sqrt((double) results.length)));
   }
      
   public static void main(String[] args) {
     int N = Integer.parseInt(args[0]);
     int T = Integer.parseInt(args[1]);
     PercolationStats ps = new PercolationStats(N, T);
     
     System.out.println("mean                    = " + ps.mean());
     System.out.println("stddev                  = " + ps.stddev());
     System.out.println("95% confidence interval = " 
                            + ps.confidenceLo() 
                            + ", " + ps.confidenceHi());
   }
}