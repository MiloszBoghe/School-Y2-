package be.pxl.examen.model;

import javax.persistence.*;
import java.time.LocalDate;

@Entity
public class Droneflight {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    @OneToOne
    private Dronepilot pilot;
    @OneToOne
    private Drone drone;
    private LocalDate date;

    public Droneflight() {
        // JPA only
    }

    public Droneflight(Dronepilot pilot, Drone drone, LocalDate date) {
        this.pilot = pilot;
        this.drone = drone;
        this.date = date;
    }

    public Drone getDrone() {
        return drone;
    }

    public LocalDate getDate() {
        return date;
    }

    public Dronepilot getPilot() {
        return pilot;
    }

}
