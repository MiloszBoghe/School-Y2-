package be.pxl.ja.Week3.Generics.Generics.Opgave1.Opgave_a;

import be.pxl.ja.Week3.Generics.Generics.Opgave1.Car;
import be.pxl.ja.Week3.Generics.Generics.Opgave1.CircularSaw;
import be.pxl.ja.Week3.Generics.Generics.Opgave1.Motorized;

public class TestWorkingPlace {
	public static void main(String[] args) {
		WorkingPlace<Car> carWorkingPlace = new WorkingPlace<>();
		//WorkingPlace<Bike> bikeWorkingPlace = new WorkingPlace<>();
		//WorkingPlace<Vehicle> vehicleWorkingPlace = new WorkingPlace<>();
		WorkingPlace<CircularSaw> otherWorkPlace = new WorkingPlace<>();
		WorkingPlace<Motorized> motorizedWorkingPlace = new WorkingPlace<>();
	}
}
