package be.pxl.ja.Week2.Demo;

import java.util.Scanner;

public class ArithmeticDemo {
    public static void main(String[] args) {
        try (Scanner keyboard = new Scanner(System.in)) {
            System.out.println("Geef een getal");
            int numerator = Integer.parseInt(keyboard.next());
            System.out.println("Geef nog een getal");
            int denominator = Integer.parseInt(keyboard.next());
            int division = numerator / denominator;
            System.out.format("%d/%d=%d", numerator, denominator, division);
        } catch (NumberFormatException ex) {
            System.out.println("Number format exception");
        } catch (ArithmeticException ex) {
            System.out.println("Don't divide by zero!");
        }
    }
}
