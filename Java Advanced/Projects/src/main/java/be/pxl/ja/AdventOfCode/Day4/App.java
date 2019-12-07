package be.pxl.ja.AdventOfCode.Day4;

import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class App implements {
    public static void main(String[] args) {
        int end = 576723;
        int count = 0;
        Pattern p = Pattern.compile("^0*1*2*3*4*5*6*7*8*9*$");
        for (int begin = 109165; begin < end; begin++) {
            int cijfer1 = begin / 100000;
            int cijfer2 = begin % 100000 / 10000;
            int cijfer3 = begin % 10000 / 1000;
            int cijfer4 = begin % 1000 / 100;
            int cijfer5 = begin % 100 / 10;
            int cijfer6 = begin % 10;

            if (!(cijfer1 > cijfer2 || cijfer2 > cijfer3 || cijfer3 > cijfer4 || cijfer4 > cijfer5 || cijfer5 > cijfer6)) {
                if()
            }
        }
        System.out.println(count);


    }
}
