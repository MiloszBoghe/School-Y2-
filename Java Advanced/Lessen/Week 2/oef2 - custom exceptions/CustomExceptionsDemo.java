import java.util.ArrayList;

public class CustomExceptionsDemo {

    public static String[] names = {"Bart", "Nele", "Sam", ""};

    public static void main(String[] args) {
        ArrayList<Person> list = initialize();
    }

    public static ArrayList<Person> initialize() {
        ArrayList<Person> personList = new ArrayList<Person>();

        for(String name : names) {
            Person person = createPerson(name);
            personList.add(person);
        }
        return personList;
    }

    public static Person createPerson(String name) {
        return new Person(name);
    }

}
