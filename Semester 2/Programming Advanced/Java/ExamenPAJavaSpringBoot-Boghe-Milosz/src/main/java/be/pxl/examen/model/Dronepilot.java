package be.pxl.examen.model;

import javax.persistence.*;

@Entity
public final class Dronepilot {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    private String email;
    @Enumerated(EnumType.STRING)
    private Droneclass droneclass;

    public Dronepilot() {
        // JPA only
    }

    public Dronepilot(String email, Droneclass droneclass) {
        this.email = email;
        this.droneclass = droneclass;
    }

    @Override
    public String toString() {
        return email;
    }

    public boolean allowedToFly(Drone drone) {
        return drone.getDroneclass().ordinal() <= droneclass.ordinal();
    }
}
