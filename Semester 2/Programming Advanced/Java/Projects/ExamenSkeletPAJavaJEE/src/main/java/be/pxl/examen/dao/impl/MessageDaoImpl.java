package be.pxl.examen.dao.impl;

import be.pxl.examen.dao.MessageDao;
import be.pxl.examen.model.Message;

import javax.persistence.EntityManager;
import javax.persistence.TypedQuery;
import java.util.List;

public class MessageDaoImpl implements MessageDao {

	private EntityManager entityManager;

	public MessageDaoImpl(EntityManager entityManager) {
		this.entityManager = entityManager;
	}

	@Override
	public List<Message> findAll() {
		TypedQuery<Message> query = entityManager.createQuery("SELECT m FROM Message m", Message.class);
		return query.getResultList();
	}

	@Override
	public long count() {
		TypedQuery<Long> query = entityManager.createQuery("SELECT COUNT(m) FROM Message m", Long.class);
		return query.getSingleResult();
	}
}
