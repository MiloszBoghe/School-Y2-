package HerhalingsOefening;

import java.io.BufferedWriter;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;

public class Writer extends Thread {
    //street,city,zip,state,beds,baths,sq__ft,type,sale_date,price,latitude,longitude
    private Path filePath;
    private List<Property> properties;

    public Writer(String filename, List<Property> properties) {
        this.filePath = Paths.get(System.getProperty("user.dir")).resolve("src/main/resources/" + filename);
        this.properties = properties;
    }

    @Override
    public void run() {
        try (BufferedWriter writer = Files.newBufferedWriter(filePath)) {
            properties.forEach(property -> {
                try{
                    writer.write(property.toFormattedOutput());
                    writer.newLine();
                }catch(IOException ex){
                    ex.printStackTrace();
                }
            });
        } catch (IOException ex) {
            ex.printStackTrace();
        }
    }
}
