package HerhalingsOefening;

import java.io.BufferedReader;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.Locale;

public class Reader {
    public static ArrayList<Property> readFile(Path path) {
        ArrayList<Property> properties = new ArrayList<>();
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("EEE MMM dd HH:mm:ss z yyyy", Locale.US);

        try (BufferedReader reader = Files.newBufferedReader(path)) {
            //eerste lijn is niet relevant
            String line = reader.readLine();
            while ((line = reader.readLine()) != null) {
                Property p = new Property();
                String[] info = line.split(",");
                //street,city,zip,state,beds,baths,sq__ft,type,sale_date,price,latitude,longitude
                p.setStreet(info[0]);
                p.setCity(info[1]);
                p.setZip(info[2]);
                p.setState(info[3]);
                p.setBedrooms(Integer.parseInt(info[4]));
                p.setBathrooms(Integer.parseInt(info[5]));
                p.setSqft(Double.parseDouble(info[6]));
                p.setType(info[7]);
                p.setSaleDate(LocalDateTime.parse(info[8],formatter));
                p.setPrice(Double.parseDouble(info[9]));
                p.setLatitude(Double.parseDouble(info[10]));
                p.setLongitude(Double.parseDouble(info[11]));
                properties.add(p);
            }
        } catch (IOException e) {
            System.out.println(e.getMessage());
        }
        return properties;
    }
}
