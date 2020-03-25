package be.pxl.jpa;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.EntityTransaction;
import javax.persistence.Persistence;

public class HibernateMain {
    public static void main(String[] args) {
        EntityManagerFactory emf = Persistence.createEntityManagerFactory("course");
        EntityManager em = emf.createEntityManager();
        EntityTransaction tx = em.getTransaction();

        tx.begin();
        em.persist(new Message("Hello virus"));
        em.persist(new Message("Hello world"));
        tx.commit();

        System.out.println(em.find(Message.class, 1L));
        System.out.println(em.find(Message.class, 2L));
    }
}
