package be.pxl.ja.opgave1;

import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Comparator;
import java.util.List;
import java.util.OptionalDouble;
import java.util.stream.Collectors;

public class TeamComposer {
    private static List<Player> players;

    public static void main(String[] args) throws PositionFullException {
        //pad maken:
        Path dataPath = Paths.get(System.getProperty("user.dir")).resolve("resources/data.csv");
        //inlezen:
        players = PlayerDataReader.readData(dataPath);
        //team aanmaken:
        Team team = new Team("PXL Dream Team", 4, 4, 2);
        //spelers filteren per rol:
        List<Striker> strikers = players.stream().filter(Striker.class::isInstance).map(Striker.class::cast).collect(Collectors.toList());
        List<Midfielder> midfielders = players.stream().filter(Midfielder.class::isInstance).map(Midfielder.class::cast).collect(Collectors.toList());
        List<Defender> defenders = players.stream().filter(Defender.class::isInstance).map(Defender.class::cast).collect(Collectors.toList());
        List<GoalKeeper> goalKeepers = players.stream().filter(GoalKeeper.class::isInstance).map(GoalKeeper.class::cast).collect(Collectors.toList());


        //add strikers:
        List<Striker> bestFinishers = strikers.stream().sorted(Comparator.comparing(Striker::getFinishing).reversed()).limit(2).collect(Collectors.toList());
        addToTeam(team, bestFinishers);
        //add midfielders:
        List<Midfielder> bestMidfielders = midfielders.stream().sorted(Comparator.comparing(Midfielder::getCombinedScore).reversed()).limit(4).collect(Collectors.toList());
        addToTeam(team, bestMidfielders);
        //add defenders:
        List<Defender> bestDefenders = defenders.stream().filter(d -> d.getNationality().equals("Italy")).sorted(Comparator.comparing(Defender::getInterceptions).reversed()).limit(4).collect(Collectors.toList());
        addToTeam(team, bestDefenders);


        //add goalkeeper:

        GoalKeeper goalkeeper = goalKeepers.stream().filter(p -> p.getName().split(" ")[1].startsWith("C")).filter(p -> p.getNationality().equals("Belgium"))
                .findFirst().get();

        team.addPlayer(goalkeeper);

        System.out.println(team.toString());
        getPlayerNationalities();
        printWageInfo();



    }

    public static <E extends Player> void addToTeam(Team team, List<E> players) throws PositionFullException {
        for (E p : players) {
            team.addPlayer(p);
        }
    }

    public static List<String> getPlayerNationalities() {
        List<String> nationalities = players.stream().map(Player::getNationality).sorted(String::compareTo).distinct().collect(Collectors.toList());
        for (String nationality : nationalities) {
            System.out.println(nationality);
        }
        return nationalities;
    }

    public static void printWageInfo() {
        double averageWage = players.stream().mapToDouble(Player::getWage).average().getAsDouble();
        double maxWage = players.stream().mapToDouble(Player::getWage).max().getAsDouble();
        System.out.println("Average wage: " + averageWage + "\nHighest wage: " + maxWage);
    }
}
