package be.pxl.ja.opgave1;

public abstract class Player {
    private String name;
    private String nationality;
    private double overall;
    private double stamina;
    private double shooting;
    private double speed;
    private String position;
    private double wage;

    public Player(String[] playerData) {
        this.name = playerData[2];
        this.nationality = playerData[5];
        this.overall = Double.parseDouble(playerData[7]);
        this.wage = 1000 * Double.parseDouble(playerData[12].substring(1,playerData[12].length()-1)); // wages are represented in K $ > multiply by 1000
        this.position = playerData[21];
        this.stamina = Double.parseDouble(playerData[71]);
        this.shooting = Double.parseDouble(playerData[73]);
        this.speed = Double.parseDouble(playerData[75]);
    }

    public String getName() {
        return name;
    }

    public String getNationality() {
        return nationality;
    }

    public double getOverall() {
        return overall;
    }

    public double getStamina() {
        return stamina;
    }

    public double getShooting() {
        return shooting;
    }

    public double getSpeed() {
        return speed;
    }

    public double getWage() {
        return wage;
    }

    public void setWage(double wage) {
        this.wage = wage;
    }

    @Override
    public String toString() {
        return String.format("%s (%s) [%f]", name, nationality, overall);
    }
}
