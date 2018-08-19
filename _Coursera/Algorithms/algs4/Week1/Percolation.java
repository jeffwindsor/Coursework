public class Percolation {
  private WeightedQuickUnionUF uf;
  private WeightedQuickUnionUF ufFill;
  private boolean[] square;
  
  private int dimension;
  private int topSiteIndex;
  private int bottomSiteIndex;
  
  public Percolation(int N) {
    dimension = N;
    int n = N * N;
    uf = new WeightedQuickUnionUF(n + 2);
    ufFill = new WeightedQuickUnionUF(n + 1);
    square = new boolean[n];
    
    //ease of use top and bottom index, so we do not have to check each row
    topSiteIndex = n;
    bottomSiteIndex = n + 1;
  }
  
  public void open(int i, int j) {
     int n = toIndexWithThrowingValidation(i, j);
     //StdOut.println("Open (" + i + "," + j + ") at Index " + n);
     if (!square[n]) {
       square[n] = true;
       
       //Connect left, right, up ,down points if they are valid and open
       connect(n, i - 1, j);
       connect(n, i + 1, j);
       connect(n, i, j - 1);
       connect(n, i, j + 1);
     }
     
     //top row - connect both
     if (i == 1)
         connect(topSiteIndex, n);
     
     //bottom row only connect normal uf
     if (i == dimension)
         uf.union(bottomSiteIndex, n);
  }
   
  public boolean isOpen(int i, int j) {
     return square[toIndexWithThrowingValidation(i, j)];
  }
   
  public boolean isFull(int i, int j) {
      int index = toIndexWithThrowingValidation(i, j);
      return square[index] && ufFill.connected(index, topSiteIndex);
  }
   
  public boolean percolates() {
     return uf.connected(topSiteIndex, bottomSiteIndex);
  }
  
   
  private void connect(int p, int i, int j) {
    if (validateCoordinate(i, j) && isOpen(i, j)) {
        int q = toIndex(i, j);
        connect(p,q);
    }
  }
  private void connect(int p, int q) {
      uf.union(p, q);
      ufFill.union(p, q);
  }
  
  private int toIndexWithThrowingValidation(int i, int j) {
      if (!validateCoordinate(i, j))
        throw new IndexOutOfBoundsException("row index i out of bounds");
      
     return toIndex(i, j);
  }
  
  private int toIndex(int i, int j) {
      return (dimension * (i - 1)) + (j - 1);
  }
    
  private boolean validateCoordinate(int i, int j) {
     return validateCoordinate(i) && validateCoordinate(j);
  }
  
  private boolean validateCoordinate(int n) {
      return (n > 0 && n <= dimension);
  }
}