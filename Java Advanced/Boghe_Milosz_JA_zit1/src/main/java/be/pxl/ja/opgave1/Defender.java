package be.pxl.ja.opgave1;

public class Defender extends Player {

    private double interceptions;
    private double standingTackle;
    private double slidingTackle;

    public Defender(String[] playerData) {
        super(playerData);
        this.interceptions = Double.parseDouble(playerData[75]);
        this.standingTackle = Double.parseDouble(playerData[81]);
        this.slidingTackle = Double.parseDouble(playerData[82]);
    }

    public double getInterceptions() {
        return interceptions;
    }

    public double getStandingTackle() {
        return standingTackle;
    }

    public double getSlidingTackle() {
        return slidingTackle;
    }

    @Override
    public String toString() {
        return String.format("DEF | %s", super.toString());
    }
}
