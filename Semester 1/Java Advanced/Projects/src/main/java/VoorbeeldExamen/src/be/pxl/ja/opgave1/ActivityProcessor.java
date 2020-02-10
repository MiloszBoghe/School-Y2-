package VoorbeeldExamen.src.be.pxl.ja.opgave1;

import java.io.*;
import java.nio.file.Files;
import java.nio.file.OpenOption;
import java.nio.file.Path;
import java.nio.file.StandardOpenOption;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.time.format.DateTimeParseException;
import java.util.ArrayList;
import java.util.List;

public class ActivityProcessor {

    private CustomerRepository customerRepository;

    public ActivityProcessor(CustomerRepository customerRepository) {
        this.customerRepository = customerRepository;
    }

    public List<Activity> processActivities(Path activityFile, Path errorFile) {
        ArrayList<Activity> activities = new ArrayList<>();

        if (activityFile.toString().contains("strava")) {
            try (BufferedReader reader = Files.newBufferedReader(activityFile)) {
                String line = null;

                while ((line = reader.readLine()) != null) {

                    String[] info = line.split("[ ;]");
                    String customerNumber = info[2];

                    if (validCustomer(customerNumber)) {
                        LocalDate datum = LocalDate.parse(info[3], DateTimeFormatter.ofPattern("dd/MM/yyyy"));
                        ActivityType type = ActivityType.valueOf(info[4].toUpperCase());
                        double distance = Double.parseDouble(info[5]);
                        ActivityTracker tracker = ActivityTracker.STRAVA;

                        //punten toevoegen
                        addPoints(type, customerNumber, distance);

                        //activity maken en toevoegen:
                        Activity activity = new Activity(customerNumber, datum, type, distance, tracker);
                        activities.add(activity);
                    } else {
                        writeError(errorFile, "Customer Number doesn't exist.");
                    }
                }
            } catch (IOException ex) {
                ex.printStackTrace();
            } catch (DateTimeParseException ex2) {
                writeError(errorFile, "Wrongly parsed date!");
            }

        } else if (activityFile.toString().contains("endomodo")) {
            try (BufferedReader reader = Files.newBufferedReader(activityFile)) {
                String line = null;
                while ((line = reader.readLine()) != null) {
                    String[] info = line.split("[;]");
                    String customerNumber = info[1];
                    Customer customer = customerRepository.getByCustomerNumber(customerNumber);
                    if (validCustomer(customerNumber)) {
                        LocalDate datum = LocalDate.parse(info[0], DateTimeFormatter.ofPattern("yyyyMMdd"));
                        ActivityType type = ActivityType.valueOf(info[2].toUpperCase());
                        double distance = Double.parseDouble(info[3]);
                        ActivityTracker tracker = ActivityTracker.ENDOMODO;

                        //punten toevoegen
                        addPoints(type, customerNumber, distance);

                        //activity maken en toevoegen:
                        Activity activity = new Activity(customerNumber, datum, type, distance, tracker);
                        activities.add(activity);
                    } else {
                        writeError(errorFile, "Customer bestaat niet.");
                    }
                }
            } catch (IOException ex) {
                System.out.println(ex.getMessage());
            } catch (IllegalArgumentException ex2) {
                writeError(errorFile, "Wrong type.");
            } catch (DateTimeParseException ex3) {
                writeError(errorFile, "Wrongly parsed date!");
            }

        } else {
            writeError(errorFile, activityFile.getFileName() + " - INVALID FILENAME");
        }

        return activities;
    }


    private void writeError(Path errorFile, String message) {
        //if folder doesn't exist, create folder.
        try {
            if (!Files.exists(errorFile.getParent())) {
                Files.createDirectory(errorFile.getParent());
            }
        } catch (IOException ex) {
            ex.getMessage();
        }

        //create writer, allow appends.
        try (BufferedWriter writer = Files.newBufferedWriter(errorFile, StandardOpenOption.CREATE, StandardOpenOption.APPEND)) {
            writer.append(message);
            writer.newLine();
        } catch (IOException ex2) {
            ex2.getMessage();
        }
    }

    private void addPoints(ActivityType type, String customerNumber, double distance) {
        Customer customer = customerRepository.getByCustomerNumber(customerNumber);
        customer.setPoints((int) (customer.getPoints() + getPoints(type, distance)));
    }

    public static double getPoints(ActivityType type, double distance) {
        switch (type) {
            case RIDING:
                return (ActivityType.RIDING.getPointsPerKm() * distance);
            case RUNNING:
                return (ActivityType.RUNNING.getPointsPerKm() * distance);
            case SWIMMING:
                return (ActivityType.SWIMMING.getPointsPerKm() * (distance / 1000));
        }
        return 0;
    }

    private boolean validCustomer(String customerNumber) {
        return customerRepository.getByCustomerNumber(customerNumber) != null;
    }
}
