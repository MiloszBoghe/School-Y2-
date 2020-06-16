package be.pxl.examen.service;

import be.pxl.examen.dao.MessageDao;
import be.pxl.examen.dao.impl.EntityManagerUtil;
import be.pxl.examen.dao.impl.MessageDaoImpl;
import be.pxl.examen.model.Message;
import be.pxl.examen.rest.resources.MessageResource;

import javax.ejb.Stateless;
import java.util.List;
import java.util.stream.Collectors;

@Stateless
public class MessageService {

	private MessageDao messageDao;

	public MessageService() {
		messageDao = new MessageDaoImpl(EntityManagerUtil.createEntityManager());
	}

	public List<MessageResource> getMessages() {
		return messageDao.findAll().stream().map(this::mapMessageToMessageResource).collect(Collectors.toList());
	}

	private MessageResource mapMessageToMessageResource(Message message) {
		MessageResource result = new MessageResource();
		result.setId(message.getId());
		result.setText(message.getText());
		return result;
	}

	public long countMessages() {
		return messageDao.count();
	}
}
