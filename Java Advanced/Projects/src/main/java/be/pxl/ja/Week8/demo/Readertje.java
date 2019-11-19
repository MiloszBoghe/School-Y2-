package be.pxl.ja.Week8.demo;

import java.io.BufferedReader;
import java.io.IOException;
import java.nio.Buffer;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class Readertje {
    public static void main(String[] args) {
        Path p = Paths.get(System.getProperty("user.home")).resolve("Opdrachten/Opdracht1/code.code");

        try {
            Stream<String> lines = Files.lines(p);
            //List<String> result =
            //      lines.sorted().distinct().collect(Collectors.toList());
            lines.flatMap(s->Stream.of(s.split(" "))).filter(l->l.toUpperCase().equals(l)).forEach(System.out::print);

        } catch (IOException ex) {
            System.out.println("Oops, something went wrong!");
            System.out.println(ex.getMessage());
        }

    }
}
