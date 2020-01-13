package be.pxl.ja.city;

public class City implements be.pxl.ja.common.DistanceFunction<City>, Comparable<City>{
    private String name;
    private double latitude;
    private double longitude;

    public City(String name, double latitude, double longitude) {
        this.name = name;
        this.latitude = latitude;
        this.longitude = longitude;
    }

    @Override
    public String toString() {
        return name;
    }

	@Override
	public double Distance(City other) {
		double radTheta = Math.toRadians(longitude - other.longitude);
		double radLatitude = Math.toRadians(latitude);
		double radOtherLatitude = Math.toRadians(other.latitude);
		double dist = Math.sin(radLatitude) * Math.sin(radOtherLatitude) + Math.cos(radLatitude) * Math.cos(radOtherLatitude) * Math.cos(radTheta);
		dist = Math.acos(dist);
		dist = Math.toDegrees(dist);
		dist = dist * 60 * 1.1515 * 1.609344;
		return dist;
	}


	//Deze compareTo zal de compareTo van klasse String gebruiken om alfabetisch te sorteren
    @Override
    public int compareTo(City city) {
        return this.name.compareTo(city.name);
    }
}
