public class WeightedQuickUnionTests {
    public static void main(String[] args) {
        WeightedQuickUnionUFX uf = new WeightedQuickUnionUFX(10);
        uf.union(4, 6);
        uf.union(8, 3);
        uf.union(9, 7);
        uf.union(9, 0);
        uf.union(1, 0);
        uf.union(8, 6);
        uf.union(1, 4);
        uf.union(0, 2);
        uf.union(7, 5);
        
        uf.print();
    }
}