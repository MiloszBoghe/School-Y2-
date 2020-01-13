package be.pxl.ja.city;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class CityTests {
    City leuven = new City("Leuven", 50.88151970000001, 4.6967578);
    City roermond = new City("Roermond", 51.19417, 5.9875);
    City maastricht = new City("Maastricht", 50.84833, 5.68889);
    City aken = new City("Aken", 50.77664, 6.08342);
    City scherpenheuvel = new City("Scherpenheuvel", 50.9890, 4.9776);

    @Test
    public void DistanceBetweenCitiesIsCorrect() {
        Assertions.assertEquals(98.08135096928707, leuven.Distance(aken));
        Assertions.assertEquals(98.08135096928707, aken.Distance(leuven));
    }

    @Test
    public void DistanceBetweenCities() {
        Assertions.assertTrue(roermond.Distance(maastricht) > 0);
        Assertions.assertTrue(roermond.Distance(scherpenheuvel) > 0);
        Assertions.assertTrue(roermond.Distance(leuven) > 0);
    }
    @Test public void DistanceBetweenCitiesIsZeroWhenSameCity(){
        Assertions.assertTrue(leuven.Distance(leuven)==0);
        Assertions.assertTrue(roermond.Distance(roermond)==0);
    }
}
