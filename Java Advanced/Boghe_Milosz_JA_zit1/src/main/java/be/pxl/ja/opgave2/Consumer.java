package be.pxl.ja.opgave2;

public class Consumer extends Thread {
    private int rate;
    private ProductionLine line;

    public Consumer(int rate, ProductionLine line) {
        this.rate = rate;
        this.line = line;
    }

    @Override
    public void run() {
        while (true) {
            Package pack = line.getPackage();
            try {
                sleep((60 / rate) * 1000);
            } catch (InterruptedException e) {
                System.out.println(e.getMessage());
            }
            System.out.println("Package verwijderd: "+pack);
        }
    }
}
