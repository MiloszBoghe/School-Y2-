package be.pxl.ja.Week8.Oefening2;

import java.io.*;

public class SpaarrekeningApp {
    public static void main(String[] args) {
        Spaarrekening rekening = new Spaarrekening(666, "BE48 321 666 999", "Milosz Boghe");
        try (FileOutputStream file = new FileOutputStream("Rekening.ser");
             ObjectOutputStream out = new ObjectOutputStream(file)) {
            out.writeObject(rekening);
        } catch (IOException ex) {
            System.out.println(ex.getMessage());
        }

        try (FileInputStream file = new FileInputStream("Rekening.ser");
             ObjectInputStream in = new ObjectInputStream(file)) {
            Spaarrekening gelezen = (Spaarrekening) in.readObject();
            System.out.println(gelezen.toString());
        } catch (IOException | ClassNotFoundException e) {
            e.printStackTrace();
        }
    }

}