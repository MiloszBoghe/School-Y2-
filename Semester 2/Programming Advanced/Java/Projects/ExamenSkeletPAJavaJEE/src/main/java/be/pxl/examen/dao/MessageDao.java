package be.pxl.examen.dao;

import be.pxl.examen.model.Message;

import java.util.List;

public interface MessageDao {

	List<Message> findAll();
	long count();
}
