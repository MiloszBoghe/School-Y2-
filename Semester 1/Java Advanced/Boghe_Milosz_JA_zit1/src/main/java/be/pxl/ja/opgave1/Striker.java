package be.pxl.ja.opgave1;

public class Striker extends Player {

    private double finishing;
    private double headingAccuracy;

    public Striker(String[] playerData) {
        super(playerData);
        this.finishing = Double.parseDouble(playerData[55]);
        this.headingAccuracy = Double.parseDouble(playerData[56]);
    }

    public double getFinishing() {
        return finishing;
    }

    public double getHeadingAccuracy() {
        return headingAccuracy;
    }

    @Override
    public String toString() {
        return String.format("STR | %s", super.toString());
    }
}
