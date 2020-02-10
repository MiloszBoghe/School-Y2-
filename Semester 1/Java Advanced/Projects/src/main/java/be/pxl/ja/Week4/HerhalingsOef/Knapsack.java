package be.pxl.ja.Week4.HerhalingsOef;

import java.util.ArrayList;
import java.util.List;

public class Knapsack<E extends Item> {
    private double maximumCapacity;
    private List<E> items;

    public Knapsack(double capacity) {
        maximumCapacity = capacity;
        items = new ArrayList<>();
    }

    public void add(E item) {
        try {
            if (item instanceof Product) {
                if (getCurrentWeight() + item.getWeight() > maximumCapacity) {
                    throw new KnapsackFullException(((Product) item).getName() + " doesn't fit in knapsack!");
                } else {
                    items.add(item);
                }
            } else{
                if (getCurrentWeight() + item.getWeight() > maximumCapacity) {
                    throw new KnapsackFullException(((Attraction) item).getName() + " doesn't fit!");
                } else {
                    items.add(item);
                }
            }
        } catch (KnapsackFullException ex) {
            System.out.println(ex.getMessage());
            ;
        }
    }

    public double getCurrentWeight() {
        double weight = 0;
        for (E item : items) {
            weight += item.getWeight();
        }
        return weight;
    }

    public List<E> getItems() {
        return items;
    }
}

