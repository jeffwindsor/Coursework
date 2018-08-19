import java.util.NoSuchElementException;
import java.util.Iterator;

public class RandomizedQueue<Item> implements Iterable<Item> {
    private Item[] items;
    private int lastIndex = -1;
    
    public RandomizedQueue() {  
        //Initial value
        items = (Item[]) new Object[100];
    }
    public boolean isEmpty()       { return lastIndex == -1; }
    public int size()              { return lastIndex + 1; }
    
    public void enqueue(Item item) { 
        if (item == null)
            throw new NullPointerException("Cannot add a null item");
        
        //Add item to end of queue
        lastIndex++;
        
        //Check inner arry is full, resize array if required
        if (lastIndex == items.length) resize(2 * items.length);
        
        items[lastIndex] = item;
    }
    public Item dequeue() {
        return getRandomItem(true);
    }
    public Item sample() {
        return getRandomItem(false);
    }
    public Iterator<Item> iterator() { return new RandomizedQueueIterator(); }
        
    private Item getRandomItem(boolean removeItem) {
        if (isEmpty())
            throw new NoSuchElementException();
        
        //Get a number between 0 and queue lenght (not array length)
        int randomIndex = StdRandom.uniform(lastIndex + 1);
        Item result = items[randomIndex];
        
        if (removeItem) {
            //Swap random index with last item
            items[randomIndex] = items[lastIndex];
            //Clear last item and decrement counter
            items[lastIndex] = null;              
            lastIndex--;
            
            //reduce array size is fallen below size of 1/4 length
            if(lastIndex > 0 && lastIndex == items.length / 4) 
                resize(items.length / 2);
        }
        return result;
    }     
    
    private void resize(int size) {
        Item[] resized = (Item[]) new Object[size];
        int limit = Math.min(size, items.length);
        for (int i = 0; i < limit; i++) {
            resized[i] = items[i];
        }
        items = resized;
    }
    
    private class RandomizedQueueIterator implements Iterator<Item> {
        private Item[] iterItems;
        private int currentIndex = 0;
        
        public RandomizedQueueIterator(){
            //Randomize the list up front.
            int length = lastIndex + 1;
            iterItems = (Item[]) new Object[length];
                        
            //Shuffle the iter array, sourcing items from the queue
            for (int i = 0; i < length; i ++) {
                iterItems[i] = items[i];
            }
            StdRandom.shuffle(iterItems);
        }
        public boolean hasNext() { return currentIndex < iterItems.length; }
        public void remove() { throw new UnsupportedOperationException(); }
        public Item next() {
            if (!hasNext())
                throw new NoSuchElementException();
            
            Item result = iterItems[currentIndex];
            currentIndex++;
            return result;
        }
    }
}