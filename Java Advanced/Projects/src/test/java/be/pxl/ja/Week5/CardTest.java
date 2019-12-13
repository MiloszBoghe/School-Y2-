package be.pxl.ja.Week5;

import be.pxl.ja.Week5.Collections.*;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

public class CardTest {

    private static final int FULL_DECK_SIZE = Suit.values().length* Value.values().length;
    private static final String LS = System.lineSeparator();

    private Deck deck;
    private Player player;
    private Card h4, c4, hj, d4, cj, c6, sa;

    @BeforeEach
    public void init() {
        deck = new Deck();
        player = new Player("Jos");
        h4 = new Card(Suit.HEARTS, Value.FOUR);
        c4 = new Card(Suit.CLUBS, Value.FOUR);
        d4 = new Card(Suit.DIAMONDS, Value.FOUR);
        c6 = new Card(Suit.CLUBS, Value.SIX);
        hj = new Card(Suit.HEARTS, Value.JACK);
        cj = new Card(Suit.CLUBS, Value.JACK);
        sa = new Card(Suit.SPADES, Value.ACE); // insert Motorhead
    }


    @Test
    public void controlDeckSize() {
        assertEquals(FULL_DECK_SIZE, deck.getSize());
    }

    @Test
    public void decksAreRandomlyShuffled() {
        Deck deck2 = new Deck();

        assertNotEquals(deck.toString(), deck2.toString());

        // Note: there's a very,very,very,VERY small chance that two decks are shuffled in exactly the same way:
        // https://math.stackexchange.com/questions/671/when-you-randomly-shuffle-a-deck-of-cards-what-is-the-probability-that-it-is-a
    }

    @Test
    public void dealingCardReducesDeckSize() {
        deck.dealCard();
        assertEquals(FULL_DECK_SIZE - 1, deck.getSize());
    }

    @Test
    public void cardsCompareSuit() {
        assertTrue(c4.compareTo(h4) > 0);
    }

    @Test
    public void cardsCompareValue() {
        assertTrue(h4.compareTo(hj) < 0);
    }

    @Test
    public void cardsCombineCompare() {
        assertTrue(d4.compareTo(cj) < 0);
    }

    @Test
    public void hasSuitReturnsTrueIfSuitAvailable() {
        player.addCard(h4);
        player.addCard(cj);

        assertTrue(player.hasSuit(Suit.HEARTS));
    }

    @Test
    public void hasSuitReturnsFalseIfSuitNotAvailable() {
        player.addCard(h4);
        player.addCard(cj);

        assertFalse(player.hasSuit(Suit.DIAMONDS));
    }

    @Test
    public void handIsAlwaysSorted() {
        player.addCard(c4);
        player.addCard(cj);
        String expected = c4 + LS + cj;
        assertEquals(expected.trim(), player.toString().trim());

        player.addCard(d4);
        expected = d4 + LS + expected;
        assertEquals(expected.trim(), player.toString().trim());

        player.addCard(hj);
        expected = hj + LS + expected;
        assertEquals(expected.trim(), player.toString().trim());

        player.addCard(c6);
        expected = hj + LS + d4 + LS + c4 + LS + c6 + LS + cj;
        assertEquals(expected.trim(), player.toString().trim());

        player.addCard(sa);
        expected = hj + LS + d4 + LS + c4 + LS + c6 + LS + cj + LS + sa;
        assertEquals(expected.trim(), player.toString().trim());
    }

}
