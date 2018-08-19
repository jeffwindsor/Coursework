public class QuickFindTests {
    public static void main(String[] args) {
        QuickFindUFX uf = new QuickFindUFX(10);
        uf.union(9, 7);
        uf.union(5,1);
        uf.union(2,5);
        uf.union(8,4);
        uf.union(2,3);
        uf.union(5,0);
        
        uf.print();
    }
}
