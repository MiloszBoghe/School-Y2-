package be.pxl.ja.Week5;

import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Deque;
import java.util.List;

public class Deck {
    private Deque<Card> cards;

    public Deck() {
        cards = new ArrayDeque<>();

        List<Card> list = new ArrayList<>();
        for (Suit suit : Suit.values()) {
            for (Value value : Value.values()) {
                list.add(new Card(suit, value));
            }
        }
    }

    public Deque<Card> getCards() {
        return cards;
    }

    public String toString() {
        for (Card c : cards) {
            System.out.println(c.getSuit() + " " + c.getValue());
        }
        return "";
    }

    public Card dealCard() {
        return null;
    }

    public int getSize() {
        return cards.size();
    }
}
