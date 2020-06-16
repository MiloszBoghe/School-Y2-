package be.pxl.examen.rest;

import be.pxl.examen.rest.resources.MessageResource;
import be.pxl.examen.service.MessageService;

import javax.inject.Inject;
import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;
import java.util.List;

@Path("messages")
public class MessageRest {

	@Inject
	private MessageService messageService;

	@GET
	@Produces(MediaType.APPLICATION_JSON)
	public List<MessageResource> getMessages() {
		return messageService.getMessages();
	}

}
