package be.pxl.examen.dao;

import be.pxl.examen.model.Dronepilot;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface DronepilotDao extends JpaRepository<Dronepilot,Long> {
    Dronepilot findByEmail(String email);
}
