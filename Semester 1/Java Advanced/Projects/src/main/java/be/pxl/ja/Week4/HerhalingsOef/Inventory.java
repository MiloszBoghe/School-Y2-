package be.pxl.ja.Week4.HerhalingsOef;

import java.util.ArrayList;
import java.util.List;

public class Inventory<T extends Item> {
    private List<T> items = new ArrayList<>();

    public void add(T object){
        items.add(object);
    }

    public List<T> getItems(){
        return items;
    }
}
