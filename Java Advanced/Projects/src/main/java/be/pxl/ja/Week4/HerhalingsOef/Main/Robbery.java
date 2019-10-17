package be.pxl.ja.Week4.HerhalingsOef.Main;

import be.pxl.ja.Week4.HerhalingsOef.Knapsack;
import be.pxl.ja.Week4.HerhalingsOef.KnapsackUtil;
import be.pxl.ja.Week4.HerhalingsOef.Product;
import be.pxl.ja.Week4.HerhalingsOef.Shop;

public class Robbery {
    public static void main(String[] args) {
        Knapsack<Product> knapsack = new Knapsack<>(45);
        Shop shop = new Shop();
       shop.add(new Product("stereo", 30, 3000));
       shop.add(new Product("laptop", 20, 2000));
       shop.add(new Product("guitar", 15, 1500));
        KnapsackUtil.fill(knapsack, shop);
        for (Product product : knapsack.getItems()) {
            System.out.println(product);
        }
    }
}