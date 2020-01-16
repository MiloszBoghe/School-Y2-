package HerhalingsOefening;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.nio.file.Path;
import java.nio.file.Paths;

public class TestApp {
    public static void main(String[] args) {
        String filename = "output.txt";
        Path path = Paths.get(System.getProperty("user.dir")).resolve("src/main/resources/"+filename);
        appendUsingBufferedWriter(path,"Yeet", 0);
    }

    private static void appendUsingBufferedWriter(Path filePath, String text, int noOfLines) {
        File file = new File(filePath.toString());
        FileWriter fr = null;
        BufferedWriter bw = null;
        try {
            // to append to file, you need to initialize FileWriter using below constructor
            fr = new FileWriter(file, true);
            bw = new BufferedWriter(fr);
            for (int i = 0; i < noOfLines; i++) {
                bw.newLine();
                // you can use write or append method
                bw.append(text);
            }

        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            try {
                bw.close();
                fr.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
}
