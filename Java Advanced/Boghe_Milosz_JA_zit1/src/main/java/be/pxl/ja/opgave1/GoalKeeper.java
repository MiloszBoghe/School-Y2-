package be.pxl.ja.opgave1;

public class GoalKeeper extends Player {

    private double diving;
    private double reflexes;

    public GoalKeeper(String[] playerData) {
       super(playerData);
       this.diving = Double.parseDouble(playerData[83]);
       this.reflexes = Double.parseDouble(playerData[87]);
    }

    public double getDiving() {
        return diving;
    }

    public double getReflexes() {
        return reflexes;
    }

    @Override
    public String toString() {
        return String.format("GK | %s", super.toString());
    }
}
