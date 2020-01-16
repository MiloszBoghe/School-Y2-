package VoorbeeldExamen.src.be.pxl.ja.opgave1;

import java.nio.file.Path;
import java.nio.file.Paths;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;
import java.util.stream.Collectors;

public class Opgave1 {
    public static void main(String[] args) {
        CustomerRepository customerRepository = new CustomerRepository();
        List<Customer> customers = customerRepository.findAll();

        System.out.println("*** Klanten uit Louisville:");
        long amountLouis = customers.stream().filter(c -> c.getCity().equals("Louisville")).count();
        System.out.println("Amount of customers in Louisville: " + amountLouis);


        System.out.println("*** Jarige klanten: ");
        customers.stream().filter(c -> c.getDateOfBirth().equals(LocalDate.now())).forEach(System.out::println);

        System.out.println("*** 10 jongste klanten:");
        customers.stream().sorted(Comparator.comparing(Customer::getDateOfBirth).reversed()).limit(10).forEach(System.out::println);

        ActivityProcessor activityFileProcessor = new ActivityProcessor(customerRepository);
        List<Activity> allActivities;

        Path activityStrava = Paths.get(System.getProperty("user.dir")).resolve("src/main/resources/opgave1/activities_from_strava.txt");
        Path activityStrava2 = Paths.get(System.getProperty("user.dir")).resolve("src/main/resources/opgave1/strava_activities.txt");
        Path activityEndomodo = Paths.get(System.getProperty("user.dir")).resolve("src/main/resources/opgave1/endomodo_activities.txt");
        Path activityRunkeeper = Paths.get(System.getProperty("user.dir")).resolve("src/main/resources/opgave1/runkeeper_activities.txt");
        Path errorPath = Paths.get(System.getProperty("user.dir")).resolve("src/main/resources/opgave1/log/errors.log");

        allActivities = activityFileProcessor.processActivities(activityStrava, errorPath);
        allActivities.addAll(activityFileProcessor.processActivities(activityStrava2, errorPath));
        allActivities.addAll(activityFileProcessor.processActivities(activityEndomodo, errorPath));
        allActivities.addAll(activityFileProcessor.processActivities(activityRunkeeper, errorPath));

        System.out.println("*** Top 10 klanten");
        ArrayList<Customer> topCustomers = customers.stream().sorted(Comparator.comparing(Customer::getPoints).reversed()).limit(10).collect(Collectors.toCollection(ArrayList::new));

        double total = 0;
        System.out.println("** Alle activiteiten meest actieve klant (gesorteerd op datum):");
        allActivities.stream().filter(a -> a.getCustomerNumber().equals(topCustomers.get(0).getCustomerNumber())).sorted(Comparator.comparing(Activity::getActivityDate).reversed()).forEach(a->{
            System.out.println(a.getActivityDate());
            System.out.println(a.getActivityType());
            System.out.println(ActivityProcessor.getPoints(a.getActivityType(),a.getDistance()));
            System.out.println();
        });

    }
}
