package be.pxl.ja.opgave2;

public class Producer extends Thread {

    private int rate;
    private ProductionLine line;

    public Producer(int rate, ProductionLine line) {
        this.rate = rate;
        this.line = line;
    }

    @Override
    public void run() {
        while (true) {
            line.addPackage();
            try {
                sleep((60 / rate) * 1000);
            } catch (InterruptedException e) {
                System.out.println(e.getMessage());
            }
            System.out.println("Package toegevoegd: "+Package.count);
        }
    }
}
