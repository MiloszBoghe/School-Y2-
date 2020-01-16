package be.pxl.ja.opgave2;

import java.util.ArrayList;
import java.util.Set;
import java.util.TreeSet;

public class ProductionLine {
    private Set<Package> packages;

    public ProductionLine() {
        packages = new TreeSet<>();
    }

    public synchronized Package getPackage() {
        Package frontPackage = null;
        try {
            frontPackage = packages.stream().findFirst().get();
            packages.remove(frontPackage);
        } catch (NullPointerException ex) {
            System.out.println("Geen pakketjes meer.");
        }
        Package.count--;
        return frontPackage;
    }

    public synchronized void addPackage() {
        packages.add(new Package());
    }
}
