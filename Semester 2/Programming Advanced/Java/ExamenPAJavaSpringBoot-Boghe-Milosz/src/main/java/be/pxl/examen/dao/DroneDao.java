package be.pxl.examen.dao;

import be.pxl.examen.model.Drone;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface DroneDao extends JpaRepository<Drone,Long> {
    //findAll is er al via JpaRepository
    //findById is er al via JpaRepository
}
