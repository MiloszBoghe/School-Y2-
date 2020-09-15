package be.pxl.examen.model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

@Entity
public class Drone {

    public static final int DRONE_WEIGHT_C1 = 250;
    public static final int DRONE_WEIGHT_C2 = 4000;

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    private String code;
    private String description;
    private int weight;

    public Drone() {
    }

    public Drone(String code, String description, int weight) {
        this.code = code;
        this.description = description;
        this.weight = weight;
    }

    public Long getId() {
        return id;
    }

    public Droneclass getDroneclass() {
        if (weight < DRONE_WEIGHT_C1) {
            return Droneclass.C0;
        } else if (weight < DRONE_WEIGHT_C2) {
            return Droneclass.C1;
        } else {
            return Droneclass.C2;
        }
    }

    public String getCode() {
        return code;
    }

    public int getWeight() {
        return weight;
    }

    public void setWeight(int weight) {
        this.weight = weight;
    }

    public String getDescription() {
        return description;
    }

    @Override
    public String toString() {
        return code + " " + getDroneclass();
    }
}
