import java.util.Iterator;

public class RandomizedQueueTests extends junit.framework.TestCase {
    public void testWhenNewThenTheQueueIsEmpty() {
        RandomizedQueue<String> q = new RandomizedQueue<String>();
        assertTrue("Empty Queue upon creation", q.isEmpty());
    }
    public void testWhenAnItemIsEnqueuedTheQueueIsNotEmpty() {
        RandomizedQueue<String> q = new RandomizedQueue<String>();
        q.enqueue("one");
        assertFalse("Non Empty Queue after add of item", q.isEmpty());
    }
    public void testWhenAnItemIsEnqueuedThenDequeuedTheQueueIsEmpty() {
        RandomizedQueue<String> q = new RandomizedQueue<String>();
        q.enqueue("one");
        q.dequeue();
        assertTrue("Empty Queue upon enqueue and dequeue", q.isEmpty());
    }
    
    public void testWhenItemsCanIterateOverAll() {
        RandomizedQueue<String> q = new RandomizedQueue<String>();
        q.enqueue("one");
        q.enqueue("one");
        q.enqueue("one");
        int count = 0;
        for(String s : q) {
            count++;
        }
            
        assertTrue("Iterate", count == 3);
    }
    
}