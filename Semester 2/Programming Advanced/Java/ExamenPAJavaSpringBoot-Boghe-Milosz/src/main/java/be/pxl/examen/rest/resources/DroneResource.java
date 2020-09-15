package be.pxl.examen.rest.resources;

public class DroneResource {
	private Long id;
	private String code;
	private String description;
	private int weight;
	private String droneclass;

	public Long getId() {
		return id;
	}

	public void setId(Long id) {
		this.id = id;
	}

	public String getCode() {
		return code;
	}

	public void setCode(String code) {
		this.code = code;
	}

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public int getWeight() {
		return weight;
	}

	public void setWeight(int weight) {
		this.weight = weight;
	}

	public String getDroneclass() {
		return droneclass;
	}

	public void setDroneclass(String droneclass) {
		this.droneclass = droneclass;
	}
}
