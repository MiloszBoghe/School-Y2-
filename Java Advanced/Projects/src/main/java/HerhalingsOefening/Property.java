package HerhalingsOefening;

import java.nio.file.Path;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.ArrayList;

public class Property {
    private String street;
    private String city;
    private String zip;
    private String state;
    private int bedrooms;
    private int bathrooms;
    private double sqft;
    private String type;
    private LocalDateTime saledate;
    private double price;
    private double latitude;
    private double longitude;

    public Property(){

    }

    //region Setters&Getters
    public String getStreet() {
        return street;
    }

    public void setStreet(String street) {
        this.street = street;
    }

    public String getCity() {
        return city;
    }

    public void setCity(String city) {
        this.city = city;
    }

    public String getZip() {
        return zip;
    }

    public void setZip(String zip) {
        this.zip = zip;
    }

    public String getState() {
        return state;
    }

    public void setState(String state) {
        this.state = state;
    }

    public int getBedrooms() {
        return bedrooms;
    }

    public void setBedrooms(int bedrooms) {
        this.bedrooms = bedrooms;
    }

    public int getBathrooms() {
        return bathrooms;
    }

    public void setBathrooms(int bathrooms) {
        this.bathrooms = bathrooms;
    }

    public double getSqft() {
        return sqft;
    }

    public void setSqft(double sqft) {
        this.sqft = sqft;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public LocalDateTime getSaledate() {
        return saledate;
    }

    public void setSaledate(LocalDateTime saledate) {
        this.saledate = saledate;
    }

    public double getPrice() {
        return price;
    }

    public void setPrice(double price) {
        this.price = price;
    }

    public double getLatitude() {
        return latitude;
    }

    public void setLatitude(double latitude) {
        this.latitude = latitude;
    }

    public double getLongitude() {
        return longitude;
    }

    public void setLongitude(double longitude) {
        this.longitude = longitude;
    }
    //endregion

    public static ArrayList<Property> propertiesAbovePrice(int price){
        return null;
    }
    public static ArrayList<Property> propertiesForZIPCode(String zip){
        return null;
    }

    public static ArrayList<Property> propertiesSoldAfter(LocalDate date){
        return null;
    }

    public static ArrayList<Property> lastPropertiesSold(int amount) {
        return null;
    }

    public static Property findCheapest(){
        return null;
    }

    public static ArrayList<Property> Read(Path path){
        return null;
    }
}
