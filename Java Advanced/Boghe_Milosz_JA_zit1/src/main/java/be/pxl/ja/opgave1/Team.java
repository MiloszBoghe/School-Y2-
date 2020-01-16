package be.pxl.ja.opgave1;

public class Team {
    private String naam;
    private GoalKeeper goalKeeper;
    private Position<Defender> defenders;
    private Position<Midfielder> midfielders;
    private Position<Striker> strikers;

    public Team(String naam, int def, int mid, int str) {
        this.naam = naam;
        defenders = new Position<>(def);
        midfielders = new Position<>(mid);
        strikers = new Position<>(str);

    }

    public void addPlayer(Player p) throws PositionFullException {
        if (p instanceof GoalKeeper) {
            if (goalKeeper == null) {
                goalKeeper = (GoalKeeper) p;
            } else {
                throw new PositionFullException("Position is full.");
            }
        } else if (p instanceof Defender) {
            if (!defenders.isFull()) {
                defenders.addPlayer((Defender) p);
            } else {
                throw new PositionFullException("Position is full.");
            }
        } else if (p instanceof Striker) {
            if (!strikers.isFull()) {
                strikers.addPlayer((Striker) p);
            } else {
                throw new PositionFullException("Position is full.");
            }
        } else if (p instanceof Midfielder) {
            if (!midfielders.isFull()) {
                midfielders.addPlayer((Midfielder) p);
            } else {
                throw new PositionFullException("Position is full.");
            }
        }

    }

    public boolean isFull() {
        return goalKeeper != null && defenders.isFull() && midfielders.isFull() && strikers.isFull();
    }

    @Override
    public String toString() {
            StringBuilder sb = new StringBuilder();
        try {
            sb.append("KEEPERS\n");
            sb.append("-----------------\n");
            sb.append(goalKeeper.toString()+"\n");
            sb.append("DEFENDERS\n");
            sb.append("-----------------\n");
            sb.append(defenders.toString());
            sb.append("MIDFIELDERS\n");
            sb.append("-----------------\n");
            sb.append(midfielders.toString());
            sb.append("STRIKERS\n");
            sb.append("-----------------\n");
            sb.append(strikers.toString());
        }catch(NullPointerException ex){
            ex.getMessage();
        }finally{
            return sb.toString();
        }
    }
}
