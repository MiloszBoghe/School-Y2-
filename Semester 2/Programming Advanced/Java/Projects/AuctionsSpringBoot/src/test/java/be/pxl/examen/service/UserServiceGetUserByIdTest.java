package be.pxl.examen.service;

import be.pxl.examen.dao.UserDao;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;

public class UserServiceGetUserByIdTest {

	@Mock
	private UserDao userDao;
	@InjectMocks
	private UserService userService;

	@BeforeEach
	public void init() {
	}

	@Test
	public void returnsNullWhenNoUserWithGivenIdFound() {

	}

	@Test
	public void returnsUserWhenUserFoundWithGivenId() {
	}
}
