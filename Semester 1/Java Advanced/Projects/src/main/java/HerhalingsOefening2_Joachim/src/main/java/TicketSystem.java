import java.io.FileWriter;
import java.io.IOException;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.*;

public class TicketSystem {

    private QueueService service;
    private HashMap<String, User> users;
    private HashMap<String, Venue> locations;
    private HashMap<String, Event> events;

    public TicketSystem () {
        service = new QueueService();
        users = new HashMap<>();
        locations = new HashMap<>();
        events = new HashMap<>();
    }

    public User getUser(String userID) {
        return users.get(userID);
    }

    public Venue getVenue(String venueId) {
        return locations.get(venueId);
    }

    public Event getEvent(String eventID) {
        return events.get(eventID);
    }

    public void addUser(User user) {
        users.put(user.getId(), user);
    }

    public void addVenue(Venue venue) {
        locations.put(venue.getId(), venue);
    }

    public void addEvent(Event event) {
        events.put(event.getId(), event);
    }

    public void requestTicket(Event event, User user) {
        service.addToQue(event.getId(), user);
    }

    public void viewNext(String eventID) {
        System.out.println(service.getNextInLine(eventID));
    }

    public void assignTickets(String eventID, int number) {
        Event event = events.get(eventID);
        for (int i = 0; i < number; i++) {
            if (event.getVenue().getCapacity() > event.getAttendees().size()) {
                User user = service.getNextInLine(eventID);
                event.addAttendee(user);
                createTickets(eventID, user);
                service.removeFromQueue(eventID);
            }
        }
    }

    public void createTickets(String eventID, User user) {
        String fileName = eventID + "_" + user.getId() + ".txt";
        Path path = Paths.get(System.getProperty("user.home"));
        Path filePath = path.resolve(fileName);
        Event event = events.get(eventID);

        try (FileWriter writer = new FileWriter(filePath.toString())) {
            writer.write("Event: ");
            writer.write(event.getName() + " ");
            writer.write(event.getDescription() + " ");
            writer.write(event.getPrice() + "");
            writer.write(event.getTime().toString() + " ");
            writer.write(event.getVenue().getCity() + " ");
            writer.write(event.getVenue().getName() + " ");
            writer.write(event.getVenue().getStreet() + " ");
            writer.write(event.getVenue().getZipCode() + " ");
            writer.write(event.getVenue().getNumber() + " ");
            writer.write(System.getProperty("line.separator"));
            writer.write(user.toString());
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void showEventByName()
    {
        System.out.println("All events by sorted name:");
        List<Event> eventList = new ArrayList<>();
        for (Map.Entry<String, Event> entry:events.entrySet()) {
            eventList.add(entry.getValue());
        }
        Comparator<Event> comparator = Comparator.comparing(Event::getName);
        eventList.sort(comparator);
        eventList.forEach(e -> System.out.println(e.getName()));
        System.out.println();
    }

    public void showFullyBookedEvents(){
        System.out.println("All fully booked events: ");
        for (Map.Entry<String, Event> entry: events.entrySet()) {
            Event event = entry.getValue();
            if (event.getVenue().getCapacity() == event.getAttendees().size()) {
                System.out.println(event.getName());
            }
        }
        System.out.println();
    }

    public void showAllEventsAttending(User user)
    {
        System.out.println("All events " + user.getFirstname() + " " + user.getLastname() + " is attending: ");
        for (Map.Entry<String, Event> entry: events.entrySet()) {
            Event event = entry.getValue();
            for (User attendee :event.getAttendees()) {
                if (attendee.getId().equals(user.getId()))
                {
                    System.out.println(event.getName());
                }
            }
        }
        System.out.println();
    }

}
