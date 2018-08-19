import java.util.Iterator;

public class Week2Exercises extends junit.framework.TestCase {
    public void testWhenNewThenTheQueueIsEmpty() {
        RandomizedQueue<String> q = new RandomizedQueue<String>();
        assertTrue("Empty Queue upon creation", q.isEmpty());
    }    
}