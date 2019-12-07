package AdventOfCode.Day4;

import java.util.HashMap;
import java.util.Map;

public class App {
    public static void main(String[] args) {
        int end = 576723;
        int count = 0;

        for (int begin = 109165; begin < end; begin++) {
            Map<Character,Integer> adjacents = new HashMap<>();
            adjacents.put('1',0);
            adjacents.put('2',0);
            adjacents.put('3',0);
            adjacents.put('4',0);
            adjacents.put('5',0);
            adjacents.put('6',0);
            adjacents.put('7',0);
            adjacents.put('8',0);
            adjacents.put('9',0);
            adjacents.put('0',0);

            String strNumber = begin + "";

            for (char c : strNumber.toCharArray()){
                adjacents;
            }

        }
        System.out.println(count);


    }
}
