package be.pxl.ja.common;

import java.util.ArrayList;
import java.util.Set;

public class DistanceUtil {
    public static <T extends DistanceFunction<T>> T findClosest(Set<T> elements, T otherElement) {
        ArrayList<T> listOfElements = new ArrayList<T>(elements);
        T smallest = null;
        double smallestDistance = 1000000;
        for (T element : listOfElements) {
            if (element != otherElement) {
                double distance = element.Distance(otherElement);
                if (distance < smallestDistance) {
                    smallestDistance = distance;
                    smallest = element;
                }
            }
        }
        return smallest;
    }
}
