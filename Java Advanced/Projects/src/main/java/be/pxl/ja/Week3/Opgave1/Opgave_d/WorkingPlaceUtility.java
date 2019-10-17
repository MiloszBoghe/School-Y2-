package be.pxl.ja.Week3.Opgave1.Opgave_d;

import be.pxl.ja.Week3.Opgave1.Bike;

public class WorkingPlaceUtility {
	public static int getScore(WorkingPlace<? extends Bike> workingPlace) {
		return workingPlace.getNumberOfThingsFixed();
	}
}
