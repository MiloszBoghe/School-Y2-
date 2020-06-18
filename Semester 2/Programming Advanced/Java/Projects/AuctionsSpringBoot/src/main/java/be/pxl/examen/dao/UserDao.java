package be.pxl.examen.dao;

import be.pxl.examen.model.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import javax.swing.text.html.Option;
import java.util.Optional;

@Repository
public interface UserDao extends JpaRepository<User, Long> {
    //save() is er al
    User findUserByEmail(String email);
    //De default findUserById geeft een optional user terug
    User findUserById(long id);
    //findAll() is er al
}
