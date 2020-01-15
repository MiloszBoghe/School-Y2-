package HerhalingsOefening;

import java.lang.reflect.Array;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.stream.Collectors;

public class App {
    private static ArrayList<Property> list;

    public static void main(String[] args) {
        //street,city,zip,state,beds,baths,sq__ft,type,sale_date,price,latitude,longitude
        Path fileToRead = Paths.get(System.getProperty("user.dir")).resolve("src/main/resources/gegevens.csv");
        list = Reader.readFile(fileToRead);

        ArrayList<Property> above = propertiesAbovePrice(70000);
        ArrayList<Property> zipCode = propertiesForZIPCode("95838");
        ArrayList<Property> soldAfter = propertiesSoldAfter(LocalDate.of(2008, 4, 21));
        //ArrayList<Property> lastSold = lastPropertiesSold(3);
        Property cheapest = findCheapest();

        //region Prints
        /*
            System.out.println("above price 70000:");
            for(Property p : above){
                System.out.println(p.toString()+"\n");
            }

            System.out.println("Zipcode 95838:");
            for (Property p : zipCode) {
                System.out.println(p.toString() + "\n");
            }

        */

        System.out.println("Sold after 21 may 2008");
        for (Property p : soldAfter) {
            System.out.println(p.toString() + "\n");
        }
        /*
            System.out.println("Last 3 sold:");
            for(Property p : lastSold){
            System.out.println(p.toString());
            }
        */
        System.out.println("cheapest:");
        System.out.println(cheapest);
        //endregion

    }

    public static ArrayList<Property> propertiesAbovePrice(int price) {
        return (ArrayList<Property>) list.stream().filter(p -> p.getPrice() > price).collect(Collectors.toList());
    }

    public static ArrayList<Property> propertiesForZIPCode(String zip) {
        return (ArrayList<Property>) list.stream().filter(p -> p.getZip().equals(zip)).collect(Collectors.toList());
    }

    public static ArrayList<Property> propertiesSoldAfter(LocalDate date) {
        return (ArrayList<Property>) list.stream().filter(p -> p.getSaleDate().toLocalDate() == date).collect(Collectors.toList());
    }

    //public static ArrayList<Property> lastPropertiesSold(int amount) {
    //  return null;
    //}

    public static Property findCheapest() {
        return list.stream().min(Comparator.comparing(Property::getPrice)).get();
    }
}
