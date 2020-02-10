import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.List;

public class EventMapper implements Mapper<Event> {
    private List<Venue> venues;

    @Override
    public Event map(String[] data) {
        Event event = null;
        int numberVenueId;
        if (data.length == 6 ){
            event = new Event(data[2], LocalDateTime.parse(data[1], DateTimeFormatter.ofPattern("ddMMyyyyHHmm")), data[3], Double.parseDouble(data[4]));
            numberVenueId = 5;
        }else {
            event = new Event(data[2], LocalDateTime.parse(data[1], DateTimeFormatter.ofPattern("ddMMyyyyHHmm")), "", Double.parseDouble(data[3]));
            numberVenueId = 4;
        }

        for (int i = 0; i < venues.size(); i++) {
            if (venues.get(i).getId().equals(data[numberVenueId])) {
                event.setVenue(venues.get(i));
                break;
            }
        }
        return event;
    }

    public void setVenues(List<Venue> venues) {
        this.venues = venues;
    }
}
