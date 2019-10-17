package be.pxl.ja.Week4.HerhalingsOef;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class KnapsackUtil {
    public static <T extends Comparable<T> & Item> void fill(Knapsack<T> sack, Inventory<T> inv) {
        Collections.sort(inv.getItems());
        for(T item : inv.getItems()){
            sack.add(item);
        }
    }
}
