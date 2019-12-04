package be.pxl.ja.AdventOfCode.Day4;

import java.util.List;

public class App {
    public static void main(String[] args) {
        int end = 576723;
        int count = 0;
        for (int begin = 109165; begin < end; begin++) {
            int cijfer1 = begin / 100000;
            int cijfer2 = begin % 100000 / 10000;
            int cijfer3 = begin % 10000 / 1000;
            int cijfer4 = begin % 1000 / 100;
            int cijfer5 = begin % 100 / 10;
            int cijfer6 = begin % 10;

            if (!(cijfer1 > cijfer2 || cijfer2 > cijfer3 || cijfer3 > cijfer4 || cijfer4 > cijfer5 || cijfer5 > cijfer6)) {
                if (cijfer1 == cijfer2 || cijfer2 == cijfer3|| cijfer3 == cijfer4 || cijfer4 == cijfer5 || cijfer5 == cijfer6) {
                    count++;
                }
            }
        }
        System.out.println(count);


    }
}
