package be.pxl.examen.rest.resources;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;

public class DroneflightCreateResource {
	private static final DateTimeFormatter FORMAT = DateTimeFormatter.ofPattern("dd/MM/yyyy");

	private String email;
	private String date;

	public void setEmail(String email) {
		this.email = email;
	}

	public void setDate(String date) {
		this.date = date;
	}

	public String getEmail() {
		return email;
	}

	public LocalDate getDate() {
		return LocalDate.parse(date, FORMAT);
	}
}
