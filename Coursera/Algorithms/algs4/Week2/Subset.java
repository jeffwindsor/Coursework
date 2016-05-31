
public class Subset {
   public static void main(String[] args) {
       int N = Integer.parseInt(args[0]);
       
       //En Queue
       RandomizedQueue<String> q = new RandomizedQueue<String>();
       for (String s : StdIn.readAllStrings()) {
           q.enqueue(s);  
       }
       
       //De Queue and print
       for (int i = 0; i < N; i++) {
           StdOut.println(q.dequeue());
       }
   }
}