public class QuickFindUFX {
    private int[] id;
    private int count;

    // instantiate N isolated components 0 through N-1
    public QuickFindUFX(int N) {
        count = N;
        id = new int[N];
        for (int i = 0; i < N; i++)
            id[i] = i;
    }

    // return number of connected components
    public int count() {
        return count;
    }

    // Return component identifier for component containing p
    public int find(int p) {
        return id[p];
    }

    // are elements p and q in the same component?
    public boolean connected(int p, int q) {
        return id[p] == id[q];
    }

    // merge components containing p and q
    public void union(int p, int q) {
        if (connected(p, q)) return;
        int pid = id[p];
        for (int i = 0; i < id.length; i++)
            if (id[i] == pid) id[i] = id[q]; 
        count--;
    }
 
    public void print() {
        for (int i = 0; i < id.length; i++) {
            StdOut.println(id[i]);
        }    
    }
   
}