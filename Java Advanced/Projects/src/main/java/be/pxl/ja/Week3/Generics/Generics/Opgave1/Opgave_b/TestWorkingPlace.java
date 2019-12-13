package be.pxl.ja.Week3.Generics.Generics.Opgave1.Opgave_b;


import be.pxl.ja.Week3.Generics.Generics.Opgave1.Bike;
import be.pxl.ja.Week3.Generics.Generics.Opgave1.Car;
import be.pxl.ja.Week3.Generics.Generics.Opgave1.Vehicle;

public class TestWorkingPlace {
	public static void main(String[] args) {
		WorkingPlace<Car> carWorkingPlace = new WorkingPlace<>();
		WorkingPlace<Bike> bikeWorkingPlace = new WorkingPlace<>();
		WorkingPlace<Vehicle> vehicleWorkingPlace = new WorkingPlace<>();
		//WorkingPlace<CircularSaw> otherWorkPlace = new WorkingPlace<>();
		//WorkingPlace<Motorized> motorizedWorkingPlace = new WorkingPlace<>();
	}
}
