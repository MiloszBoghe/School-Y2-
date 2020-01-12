package HerhalingsOefening;

import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;

public class App {
    public static void main(String[] args) {

        Path fileToRead = Paths.get(System.getProperty("user.dir")).resolve("src/main/resources/gegevens.csv");
        ArrayList<Property> properties = Property.Read(fileToRead);

    }
}
