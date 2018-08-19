import java.util.NoSuchElementException;
import java.util.Iterator;

public class Deque<Item> implements Iterable<Item> {
    private Node<Item> first, last;
    private int dequeSize;
    
    public Deque() {
        dequeSize  = 0;
        last   = null;
        first = null;
    }
    
    public boolean isEmpty() { return dequeSize == 0; }    
    public int size()        { return dequeSize; }
    
    public void addFirst(Item item) {
        if (item == null)
            throw new NullPointerException("Cannot add a null item");
        
        //Create new LI
        Node<Item> next = first;
        first = new Node<Item>(item, null, next);
        
        //If any empty queue then the last is the first
        if (isEmpty()) last = first;
        else           next.previous = first;
        
        //Increment size
        dequeSize++;
    }
    
    public void addLast(Item item) {
        if (item == null)
            throw new NullPointerException("Cannot add a null item");
        
        //Put new item last
        Node<Item> previous = last;
        last = new Node<Item>(item, previous, null);
        
        // Connect previous to new item, unless empty, 
        // in which case the first is the last
        if (isEmpty()) first = last;
        else           previous.next = last;
                
        //Increment size
        dequeSize++;
    }
    
    public Item removeFirst() {
        if (isEmpty())
            throw new NoSuchElementException();

        //Decrement size
        dequeSize--;
        
        //Create new LI
        Node<Item> result = first;
        
        //Point beginning to next element
        first = result.next;
        
        //Empty case
        if (isEmpty()) last = first;
        else           first.previous = null;
        
        return result.item;
    }
    
    public Item removeLast() {
       if (isEmpty())
            throw new NoSuchElementException();
       //Decrement size
        dequeSize--; 
       
        //Create new LI
        Node<Item> result = last;
        
        //Point beginning to next element
        last = result.previous;
        
        //Empty case
        if (isEmpty()) first = last;
        else           last.next = null;
        
        return result.item;
    }
    
    public Iterator<Item> iterator() { return new DequeIterator(); }
       
    private class Node<Item> {
       public Item item;
       public Node<Item> next;
       public Node<Item> previous;
       public Node(Item item, Node<Item> previous, Node<Item> next) {
           this.item     = item;
           this.previous = previous;
           this.next     = next;
       }
    }
    private class DequeIterator implements Iterator<Item> {
        private Node<Item> current = first;
        public boolean hasNext() { return current != null; }
        public void remove() { throw new UnsupportedOperationException(); }
        public Item next() {
            if (!hasNext())
                throw new NoSuchElementException();
            
            Item result = current.item;
            current     = current.next;
            return result;
        }
    }
}