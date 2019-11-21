package be.pxl.ja.Week8.Oefening1;

import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Scanner;

public class PhoneApp {
    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);
        // TODO create a collection

        // TODO load collection from file
        Path p = Paths.get(System.getProperty("user.home")).resolve("Desktop\\School-Y2-\\Java Advanced\\Data\\phonedirectory.txt");

        boolean running = true;
        while (running) {
            System.out.println("Geef een naam: ");
            String naam = input.nextLine();

            // TODO do stuff

            System.out.println("Geef een telefoonnummer: ");
            String telnr = input.nextLine();

            // TODO do stuff

            System.out.println("Wilt u nog namen toevoegen? (j/n)");
            running = input.nextLine().equals("j");
        }


        // TODO save contacts to file

        input.close();
    }
}
