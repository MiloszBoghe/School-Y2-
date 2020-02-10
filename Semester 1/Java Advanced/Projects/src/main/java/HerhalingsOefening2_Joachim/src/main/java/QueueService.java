import java.util.ArrayDeque;
import java.util.HashMap;

public class QueueService {

    private HashMap<String, ArrayDeque<User>> queue = new HashMap<>();

    public void addToQue(String eventID, User user) {
        if (!queue.containsKey(eventID)) {
            queue.put(eventID, new ArrayDeque<>());
        }
        queue.get(eventID).add(user);
    }

    public ArrayDeque<User> getQueue(String eventID) {
        if (queue.containsKey(eventID)) {
            return queue.get(eventID);
        }
        return null;
    }

    public User getNextInLine(String eventID)  {
        if (queue.containsKey(eventID)) {
            return getQueue(eventID).peek();
        }
        return null;
    }

    public void removeFromQueue(String eventID) {
        queue.get(eventID).remove();
    }

    public void printQueue(String eventID) {
        if (queue.containsKey(eventID)) {
            queue.get(eventID).forEach(System.out::println);
        }
    }

    public int getQueueSize(String eventID) {
        if (queue.containsKey(eventID)) {
            queue.get(eventID).size();
        }
        return 0;
    }

}
