package be.pxl.ja.Week8.demo;

import java.io.BufferedReader;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class test {
    public static void main(String[] args) {
        Path p = Paths.get(System.getProperty("user.home")).resolve("Opdrachten/Opdracht2/bijlage1.txt");
        Path p2 = Paths.get("resources/data.txt");

        //BufferedReader gebruik:
       /* try (BufferedReader reader = Files.newBufferedReader(p)) {
            String line = null;
            while ((line = reader.readLine()) != null) {
                System.out.println(line);
            }

        } catch (IOException ex) {
            System.out.println("Oops, something went wrong!");
            System.out.println(ex.getMessage());
        }finally{

        }

        */


        //Stream gebruik:
        try {
            Stream<String> lines = Files.lines(p);
            //List<String> result =
              //      lines.sorted().distinct().collect(Collectors.toList());
            lines.filter(l->l.toUpperCase()==l).forEach(System.out::print);

        } catch (IOException ex) {
            System.out.println("Oops, something went wrong!");
            System.out.println(ex.getMessage());
        }



        //Stream gebruik maar in een list opslaan:
        /*
        try {
            Stream<String> lines = Files.lines(p);
            List<String> result =
                    lines.sorted().distinct().collect(Collectors.toList());

        } catch (IOException ex) {
            System.out.println("Oops, something went wrong!");
            System.out.println(ex.getMessage());
        }
         */


    }
}
