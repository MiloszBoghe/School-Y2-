package Hackathon;

import java.util.Scanner;
import java.util.TreeSet;

public class Overdraft {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        int aantal = 0;
        aantal = scanner.nextInt();
        int[] saldo = new int[aantal];

        for (int i = 0; i < aantal; i++) {
            saldo[i] = scanner.nextInt();
            int aantalTransactions = scanner.nextInt();
            TreeSet<Integer> transactions = new TreeSet<>();

            for (int j = 0; j < aantalTransactions; j++) {
                int amount = scanner.nextInt();
                transactions.add(amount);
            }

            for (Integer integer : transactions) {
                saldo[i] += integer;
                if (saldo[i] < 0) saldo[i] -= 35;
            }
        }

        for (int a = 0; a < aantal; a++) {
            System.out.println(saldo[a]);
        }

    }
}
