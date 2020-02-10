package be.pxl.ja.opgave2;

public class Package implements Comparable<Package> {
    public static int count = 0;

    private int id;

    public Package() {
        this.id = count++;
    }

    @Override
    public String toString() {
        return "#" + this.id;
    }

    @Override
    public int compareTo(Package pack) {
        return Integer.compare(id, pack.id);
    }
}
