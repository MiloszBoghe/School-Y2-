package HerhalingsOefening;

import java.nio.file.Path;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.Locale;

public class Property {
    private String street;
    private String city;
    private String zip;
    private String state;
    private int bedrooms;
    private int bathrooms;
    private double sqft;
    private String type;
    private LocalDateTime saleDate;
    private double price;
    private double latitude;
    private double longitude;

    public Property() {

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

    public LocalDateTime getSaleDate() {
        return saleDate;
    }

    public void setSaleDate(LocalDateTime saleDate) {
        this.saleDate = saleDate;
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

    @Override
    public String toString() {
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd/MMM/yyyy", Locale.US);
        return "ZipCode: " + zip + "\nPrice: " + price + "\nDate sold: " + saleDate.format(formatter);
    }


    public String toFormattedOutput() {
        // street,city,zip,state,beds,baths,sq__ft,type,sale_date,price,latitude,longitude
        return  getStreet() + "," +
                getCity() + "," +
                getZip() + "," +
                getState() + "," +
                getBedrooms() + "," +
                getBathrooms() + "," +
                getSqft() + "," +
                getType() + "," +
                getSaleDate() + "," +
                getPrice() + "," +
                getLatitude() + "," +
                getLongitude();
    }
}
