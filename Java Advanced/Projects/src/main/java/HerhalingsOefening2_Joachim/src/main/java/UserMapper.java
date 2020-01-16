import java.time.LocalDate;
import java.time.format.DateTimeFormatter;

public class UserMapper implements Mapper<User> {
    @Override
    public User map(String[] data) {
        User user = new User(data[1], data[2], LocalDate.parse(data[3], DateTimeFormatter.ofPattern("ddMMyyyy")));
        user.setId(data[0]);
        return user;
    }
}
