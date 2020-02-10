package be.pxl.ja.Week5.Collections;

import java.util.Set;
import java.util.TreeSet;

public class Player {
    private String name;
    private Set<Card> hand;

    public Player(String name) {
        this.name = name;
        hand = new TreeSet<Card>();
    }

    public String getName() {
        return name;
    }

    public void addCard(Card card) {
        hand.add(card);
    }

    public boolean hasSuit(Suit suit) {
        for (Card card : hand) {
            if(card.getSuit()==suit){
                return true;
            }
        }
        return false;
    }
}
