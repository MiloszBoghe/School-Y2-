package be.pxl.ja.city;

import be.pxl.ja.common.DistanceUtil;

import java.util.TreeSet;

public class DistancesBetweenCities {

    public static void main(String[] args) {
        City leuven = new City("Leuven", 50.88151970000001, 4.6967578);
        City roermond = new City("Roermond", 51.19417, 5.9875);
        City maastricht = new City("Maastricht", 50.84833, 5.68889);
        City aken = new City("Aken", 50.77664, 6.08342);
        City scherpenheuvel = new City("Scherpenheuvel", 50.9890, 4.9776);

        //TreeSet sorteert automatisch via de compareTo methode van het object.
        //in dit geval City, de compareTo methode sorteert alfabetisch.
        TreeSet<City> cities = new TreeSet<>();
        cities.add(leuven);
        cities.add(roermond);
        cities.add(maastricht);
        cities.add(aken);
        cities.add(scherpenheuvel);

        System.out.println("\n" + cities + "\nThe closest city to Leuven is " + DistanceUtil.findClosest(cities, aken));
        System.out.println(leuven.Distance(aken));
    }
}
