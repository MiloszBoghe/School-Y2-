package be.pxl.ja.opgave1;

public class Midfielder extends Player {
    private double shortPassing;
    private double longPassing;
    private double ballControl;

    public Midfielder(String[] playerData) {
        super(playerData);
        this.shortPassing = Double.parseDouble(playerData[57]);
        this.longPassing = Double.parseDouble(playerData[62]);
        this.ballControl = Double.parseDouble(playerData[63]);
    }

    public double getShortPassing() {
        return shortPassing;
    }

    public double getLongPassing() {
        return longPassing;
    }

    public double getBallControl() {
        return ballControl;
    }

    public double getCombinedScore() {
        return ballControl + shortPassing + longPassing;
    }

    @Override
    public String toString() {
        return String.format("MID | %s", super.toString());
    }
}
