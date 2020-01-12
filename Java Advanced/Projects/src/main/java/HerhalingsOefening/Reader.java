package HerhalingsOefening;

import java.io.BufferedReader;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.ArrayList;

public class Reader {
    public static ArrayList<Property> readFile(Path path) {
        ArrayList<Property> properties = new ArrayList<>();
        try (BufferedReader reader = Files.newBufferedReader(path)) {
            String line = reader.readLine();
            while ((line = reader.readLine()) != null) {
                Property p = new Property();

                properties.add(p);
            }
        } catch (IOException e) {
            System.out.println(e.getMessage());
        }
    }
}
