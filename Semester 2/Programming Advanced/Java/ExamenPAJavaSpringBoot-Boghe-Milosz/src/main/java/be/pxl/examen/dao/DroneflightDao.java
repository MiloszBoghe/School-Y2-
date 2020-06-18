package be.pxl.examen.dao;

import be.pxl.examen.model.Drone;
import be.pxl.examen.model.Droneflight;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.time.LocalDate;

@Repository
public interface DroneflightDao extends JpaRepository<Droneflight,Long> {
    Droneflight findByDroneAndDate(Drone drone, LocalDate date);
}
