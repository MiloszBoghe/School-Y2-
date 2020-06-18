package be.pxl.examen.service;

import be.pxl.examen.dao.UserDao;
import be.pxl.examen.model.User;
import be.pxl.examen.rest.resource.UserCreateResource;
import be.pxl.examen.rest.resource.UserResource;
import be.pxl.examen.util.EmailValidator;
import be.pxl.examen.util.exception.DuplicateEmailException;
import be.pxl.examen.util.exception.InvalidDateException;
import be.pxl.examen.util.exception.InvalidEmailException;
import be.pxl.examen.util.exception.RequiredFieldException;
import org.apache.commons.lang3.StringUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.time.format.DateTimeParseException;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class UserService {
	@Autowired
	private UserDao userDao;
	private static final DateTimeFormatter DATE_FORMAT = DateTimeFormatter.ofPattern("dd/MM/uuuu");


	public List<UserResource> getAllUsers() {
		return userDao.findAll().stream().map(this::mapToUserResource).collect(Collectors.toList());
	}

	public UserResource getUserById(long userId) {
		return mapToUserResource(userDao.findUserById(userId));
	}

	public UserResource createUser(UserCreateResource userCR) throws RequiredFieldException, InvalidEmailException, InvalidDateException, DuplicateEmailException {
		User user = mapToUser(userCR);
		if (StringUtils.isBlank(user.getFirstName())) {
			throw new RequiredFieldException("FirstName");
		}
		if (StringUtils.isBlank(user.getLastName())) {
			throw new RequiredFieldException("LastName");
		}
		if (StringUtils.isBlank(user.getEmail())) {
			throw new RequiredFieldException("Email");
		}
		if (!EmailValidator.isValid(user.getEmail())) {
			throw new InvalidEmailException(user.getEmail());
		}
		if (user.getDateOfBirth() == null) {
			throw new RequiredFieldException("DateOfBirth");
		}
		if (user.getDateOfBirth().isAfter(LocalDate.now())) {
			throw new InvalidDateException("DateOfBirth cannot be in the future.");
		}
		User existingUser = userDao.findUserByEmail(user.getEmail());
		if (existingUser != null) {
			throw new DuplicateEmailException(user.getEmail());
		}

		return mapToUserResource(userDao.save(user));
	}

	//region Mapping

	private User mapToUser(UserCreateResource userCreateResource) throws InvalidDateException {
		User user = new User();
		user.setFirstName(userCreateResource.getFirstName());
		user.setLastName(userCreateResource.getLastName());
		try {
			user.setDateOfBirth(LocalDate.parse(userCreateResource.getDateOfBirth(), DATE_FORMAT));
		} catch (DateTimeParseException e) {
			throw new InvalidDateException("[" + user.getDateOfBirth() + "] is not a valid date. Excepted format: dd/mm/yyyy");
		}
		user.setEmail(userCreateResource.getEmail());
		return user;
	}

	private UserResource mapToUserResource(User user) {
		UserResource userResource = new UserResource();
		userResource.setId(user.getId());
		userResource.setFirstName(user.getFirstName());
		userResource.setLastName(user.getLastName());
		userResource.setDateOfBirth(user.getDateOfBirth());
		userResource.setAge(user.getAge());
		userResource.setEmail(user.getEmail());
		return userResource;
	}

	//endregion
}
