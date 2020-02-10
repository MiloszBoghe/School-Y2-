package be.pxl.ja.Week3.Generics.Generics.Opgave1.Opgave_e;


public class WorkingPlaceUtility {
	public static int getScore(WorkingPlace<?> workingPlace) {
		return workingPlace.getNumberOfThingsFixed();
	}
}
