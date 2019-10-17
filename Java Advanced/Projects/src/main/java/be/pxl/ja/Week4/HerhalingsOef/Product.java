package be.pxl.ja.Week4.HerhalingsOef;

public class Product implements Item, Comparable<Product> {
    private String name;
    private double weight;
    private double price;

    public Product(String name, double weight, double price) {
        setName(name);
        setWeight(weight);
        setPrice(price);
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public double getWeight() {
        return weight;
    }

    public void setWeight(double weight) {
        this.weight = weight;
    }

    public double getPrice() {
        return price;
    }

    public void setPrice(double price) {
        this.price = price;
    }

    public String toString() {
        return "Name: " + name + " Weight: " + weight + " Price: €" + price;
    }

    public int compareTo(Product product) {
        return Double.compare(product.price, this.price);
    }
}
