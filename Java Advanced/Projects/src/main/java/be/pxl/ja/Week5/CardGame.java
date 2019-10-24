package be.pxl.ja.Week5;

//import jdk.swing.interop.SwingInterOpUtils;

public class CardGame {
    public static final int CARDS_TO_ADD = 5;

    public static void main(String[] args) {
        Deck deck = new Deck();

        Player player = new Player("Sam");

        System.out.println(deck.dealCard());

        for(int i=0;i<CARDS_TO_ADD;i++) {
            player.addCard(deck.dealCard());
        }

        System.out.println();
        System.out.println(player.getName() + "'s hand:");
        System.out.println(player);

        System.out.println("HEARTS available? " + player.hasSuit(Suit.HEARTS));

        System.out.println("Cards left in deck: " + deck.getSize());
        System.out.println(deck);
    }
}
