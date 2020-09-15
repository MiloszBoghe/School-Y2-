package be.pxl.examen.service;

import be.pxl.examen.model.Drone;
import be.pxl.examen.model.Droneclass;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.junit.jupiter.MockitoExtension;


@ExtendWith(MockitoExtension.class)
public class DroneServiceTest {

    private Drone drone;

    @BeforeEach
    public void Setup() {
        drone = new Drone();
    }

    @Test
    public void getDroneclass_Returns_C2_If_Weight_GreaterThan_4000() {
        drone.setWeight(5000);

        Droneclass result = drone.getDroneclass();

        Assertions.assertEquals(Droneclass.C2, result);
    }

    @Test
    public void getDroneclass_Returns_C1_If_Weight_Between_250_And_4000() {
        drone.setWeight(2000);

        Droneclass result = drone.getDroneclass();

        Assertions.assertEquals(Droneclass.C1, result);
    }

}
