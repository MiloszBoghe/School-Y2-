package be.pxl.ja.Week6.InnerClassesDemo;

public class Musician {

    public Musician() {

    }

    public void play() {
        Instrument anony = new Instrument() {
            @Override
            public void makeNoise() {
                System.out.println("");
            }
        };
    }


}
