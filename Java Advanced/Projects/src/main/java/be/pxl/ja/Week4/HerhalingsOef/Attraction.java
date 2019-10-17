package be.pxl.ja.Week4.HerhalingsOef;

public class Attraction implements Item, Comparable<Attraction>{
    private String name;
    private double days;
    private int rating;

    public Attraction(String name, double days, int rating){
        setName(name);
        setDays(days);
        setRating(rating);
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getName() {
        return name;
    }

    public double getWeight() {
        return days;
    }

    public void setDays(double days) {
        this.days = days;
    }

    public void setRating(int rating) {
        this.rating = rating;
    }

    public int getRating() {
        return rating;
    }


    public int compareTo(Attraction attraction) {
        return Integer.compare(attraction.rating, this.rating);
    }

    public String toString() {
        return "Name: "+ name+", Time: "+days+", Rating: "+rating;
    }
}
