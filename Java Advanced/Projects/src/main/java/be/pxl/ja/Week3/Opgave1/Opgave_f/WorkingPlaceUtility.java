package be.pxl.ja.Week3.Opgave1.Opgave_f;



public class WorkingPlaceUtility {
	public static int getScore(WorkingPlace<?> workingPlace) {
		return workingPlace.getNumberOfThingsFixed();
	}
}
