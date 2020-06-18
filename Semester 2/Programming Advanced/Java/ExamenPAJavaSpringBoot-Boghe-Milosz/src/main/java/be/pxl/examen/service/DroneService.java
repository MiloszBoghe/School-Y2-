package be.pxl.examen.service;

import be.pxl.examen.dao.DroneDao;
import be.pxl.examen.dao.DroneflightDao;
import be.pxl.examen.dao.DronepilotDao;
import be.pxl.examen.exception.DroneUnavailableException;
import be.pxl.examen.exception.NotQualifiedException;
import be.pxl.examen.model.Drone;
import be.pxl.examen.model.Droneflight;
import be.pxl.examen.model.Dronepilot;
import be.pxl.examen.rest.resources.DroneResource;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.LocalDate;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class DroneService {

	@Autowired
	private DroneDao droneDao;
	@Autowired
	private DronepilotDao dronepilotDao;
	@Autowired
	private DroneflightDao droneflightDao;

	public List<DroneResource> getDrones() {
		return droneDao.findAll().stream().map(this::mapDroneToDroneResource).collect(Collectors.toList());
	}

	private DroneResource mapDroneToDroneResource(Drone drone) {
		DroneResource droneResource = new DroneResource();
		droneResource.setId(drone.getId());
		droneResource.setCode(drone.getCode());
		droneResource.setWeight(drone.getWeight());
		droneResource.setDescription(drone.getDescription());
		droneResource.setDroneclass(drone.getDroneclass().name());
		return droneResource;
	}

	public void scheduleDroneflight(Long droneId, String pilot, LocalDate date) throws DroneUnavailableException, NotQualifiedException {
		Dronepilot piloot = dronepilotDao.findByEmail(pilot);
		Drone drone = droneDao.findById(droneId).get();

		if (!piloot.allowedToFly(drone)) {
			throw new NotQualifiedException("Dronepilot [" + piloot + "] is not qualified to fly drone [" + drone.getDescription() + "]");
		}
		Droneflight byDroneAndDatum = droneflightDao.findByDroneAndDate(drone, date);
		if (byDroneAndDatum != null) {
			throw new DroneUnavailableException("Drone [" + drone.getDescription() + "] is not available at [" + date + "]");
		}

		Droneflight vlucht = new Droneflight(piloot, drone, date);
		droneflightDao.save(vlucht);
	}

	public long countDroneflights() {
		return droneflightDao.count();
	}

}
