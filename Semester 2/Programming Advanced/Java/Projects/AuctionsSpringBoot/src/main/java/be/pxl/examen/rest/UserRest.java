package be.pxl.examen.rest;

import be.pxl.examen.rest.resource.UserCreateResource;
import be.pxl.examen.rest.resource.UserResource;
import be.pxl.examen.service.UserService;
import be.pxl.examen.util.exception.DuplicateEmailException;
import be.pxl.examen.util.exception.InvalidDateException;
import be.pxl.examen.util.exception.InvalidEmailException;
import be.pxl.examen.util.exception.RequiredFieldException;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@RequestMapping("/users")
@RestController
public class UserRest {
    private static final Logger LOGGER = LogManager.getLogger(UserRest.class);

    @Autowired
    private UserService userService;

    @GetMapping
    public List<UserResource> getAllUsers() {
        List<UserResource> result = userService.getAllUsers();
        return result;
    }

    @GetMapping("{userId}")
    public UserResource getUserById(@PathVariable("userId") long userId) {
        UserResource user = userService.getUserById(userId);
        return user;
    }


    @PostMapping
    public UserResource createUser(UserCreateResource userCreateResource) {
        try {
            UserResource user = userService.createUser(userCreateResource);
            return user;
        } catch (RequiredFieldException | InvalidEmailException | DuplicateEmailException | InvalidDateException e) {
            LOGGER.error(e.getMessage(), e);
            throw new ResponseStatusException(HttpStatus.BAD_REQUEST, e.getMessage());
        }
    }

}
