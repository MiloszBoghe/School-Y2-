package be.pxl.ja.opgave1;

import java.util.ArrayList;
import java.util.List;

public class Position<P extends Player> {
    private List<P> players;
    private int limit;

    public Position(int limit) {
        this.limit = limit;
        players = new ArrayList<P>();
    }

    public void addPlayer(P p) throws PositionFullException {
        if (isFull()) {
            throw new PositionFullException("Position limit reached.");
        } else {
            players.add(p);
        }
    }

    public List<P> getPlayers() {
        return players;
    }

    public boolean isFull() {
        return players.size() == limit;
    }

    @Override
    public String toString() {
        StringBuilder sb = new StringBuilder();
        for (P p : players) {
            sb.append(p.toString());
            sb.append("\n");
        }
        return sb.toString();
    }
}
