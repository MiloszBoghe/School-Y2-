package be.pxl.ja.Week4.HerhalingsOef.Main;

import be.pxl.ja.Week4.HerhalingsOef.*;

public class VisitLondon {
    public static void main(String[] args) {

        Knapsack<Attraction> knapsack = new Knapsack<>(2);
        LondonAttractions attractions = new LondonAttractions();
        attractions.add(new Attraction("Westminster Abbey", 0.5, 7));
        attractions.add(new Attraction("Globe Theater", 0.5, 6));
        attractions.add(new Attraction("National Gallery", 1, 9));
        attractions.add(new Attraction("British Museum", 2, 9));
        attractions.add(new Attraction("St. Paul's Cathedral", 0.5, 8));
        KnapsackUtil.fill(knapsack, attractions);
        for (Attraction attraction : knapsack.getItems()) {
            System.out.println(attraction);
        }
    }
}