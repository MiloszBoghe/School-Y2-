package be.pxl.ja.opgave1;

import java.io.BufferedReader;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class PlayerDataReader {

    private static List<String> goalkeeper = new ArrayList<>(Arrays.asList("GK"));
    private static List<String> defender = new ArrayList<>(Arrays.asList("RB", "CB", "LB", "RWB", "LWB", "LCB", "RCB"));
    private static List<String> midfielder = new ArrayList<>(Arrays.asList("LM", "RM", "CM", "CDM", "LCM", "CAM", "LDM", "LAM", "RDM", "RAM", "LW", "RW", "RCM"));
    private static List<String> striker = new ArrayList<>(Arrays.asList("ST", "LS", "RS", "CF", "RF", "LF"));

    public static List<Player> readData(Path dataPath) {
        List<Player> players = new ArrayList<>();
        try (BufferedReader reader = Files.newBufferedReader(dataPath)) {
            String line = reader.readLine();
            while ((line = reader.readLine()) != null) {
                players.add(createPlayer(line));
            }
        } catch (IOException ex) {
            System.out.println(ex.getMessage());
        } catch (PlayerCreationException e) {
            e.getMessage();
        }
        return players;
    }

    public static Player createPlayer(String line) throws PlayerCreationException {
        String[] playerData = line.split(",");

        try {
            String position = playerData[21];
            if (goalkeeper.contains(position)) {
                return new GoalKeeper(playerData);
            } else if (defender.contains(position)) {
                return new Defender(playerData);
            } else if (midfielder.contains(position)) {
                return new Midfielder(playerData);
            } else if (striker.contains(position)) {
                return new Striker(playerData);
            } else {
                throw new PlayerCreationException("Could not create player: Unknown position");
            }
        } catch (IndexOutOfBoundsException e) {
            throw new PlayerCreationException("Could not create player: Invalid data");
        }
    }
}
