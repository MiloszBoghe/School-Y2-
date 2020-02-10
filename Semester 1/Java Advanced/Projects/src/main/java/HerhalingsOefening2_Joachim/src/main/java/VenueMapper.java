public class VenueMapper implements Mapper<Venue> {
    @Override
    public Venue map(String[] data) {
        Venue venue = new Venue(data[1], data[2], data[3], data[4], data[5], Integer.parseInt(data[6]));
        venue.setId(data[0]);
        return venue;
    }
}
