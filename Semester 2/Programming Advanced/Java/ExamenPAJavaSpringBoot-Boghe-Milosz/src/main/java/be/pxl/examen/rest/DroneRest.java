package be.pxl.examen.rest;

import be.pxl.examen.exception.DroneUnavailableException;
import be.pxl.examen.exception.NotQualifiedException;
import be.pxl.examen.rest.resources.DroneResource;
import be.pxl.examen.rest.resources.DroneflightCreateResource;
import be.pxl.examen.service.DroneService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@RestController
@RequestMapping(path = "drones")
public class DroneRest {

    @Autowired
    private DroneService droneService;

    @GetMapping
    public List<DroneResource> getAllDrones() {
        return droneService.getDrones();
    }

    @PostMapping("{droneId}")
    @ResponseStatus(HttpStatus.CREATED)
    public void addDroneflight(@PathVariable long droneId, @RequestBody DroneflightCreateResource droneFlight) {
        try {
            droneService.scheduleDroneflight(droneId, droneFlight.getEmail(), droneFlight.getDate());
        }catch(DroneUnavailableException | NotQualifiedException e){
            throw new ResponseStatusException(HttpStatus.BAD_REQUEST,e.getMessage());
        }
    }

}
