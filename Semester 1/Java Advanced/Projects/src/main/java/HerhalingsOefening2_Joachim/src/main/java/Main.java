import java.io.BufferedReader;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;

public class Main {
    public static void main(String[] args) {
        TicketSystem ticketSystem = new TicketSystem();
        List<User> users =  getAllDataFromFile(new UserMapper(), Paths.get("data/userdata.txt").toAbsolutePath());
        List<Venue> venues = getAllDataFromFile(new VenueMapper(), Paths.get("data/venueData.txt").toAbsolutePath());
        EventMapper eventMapper = new EventMapper();
        eventMapper.setVenues(venues);
        List<Event> events = getAllDataFromFile(eventMapper, Paths.get("data/eventData.txt").toAbsolutePath());

        for (int i = 0; i < events.size(); i++) {
            ticketSystem.addEvent(events.get(i));
        }

        for (int i = 0; i < users.size(); i++) {
            ticketSystem.requestTicket(events.get(0), users.get(i));
        }



        ticketSystem.assignTickets(events.get(0).getId(), 5);

        users.stream().forEach(System.out::println);
        System.out.println();
        ticketSystem.showEventByName();
        ticketSystem.showFullyBookedEvents();
    }

    public static <T> List<T> getAllDataFromFile(Mapper<T> mapper, Path filePath) {
        List<T> list = new ArrayList<>();
        String[] lines = null;
        try (BufferedReader reader = Files.newBufferedReader(filePath)) {
            String line = null;
            while ((line = reader.readLine()) != null) {
                lines = line.split(";");
                list.add(mapper.map(lines));
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
        return list;
    }

}
