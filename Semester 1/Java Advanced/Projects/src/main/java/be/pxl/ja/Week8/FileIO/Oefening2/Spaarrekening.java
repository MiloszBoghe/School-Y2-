package be.pxl.ja.Week8.FileIO.Oefening2;

public class Spaarrekening implements java.io.Serializable {
    private double saldo;
    private String nummer;
    private String naam;

    public Spaarrekening(double saldo, String nummer, String naam) {
        this.saldo = saldo;
        this.nummer = nummer;
        this.naam = naam;
    }

    public double getSaldo() {
        return saldo;
    }

    public void setSaldo(double saldo) {
        this.saldo = saldo;
    }

    public String getNummber() {
        return nummer;
    }

    public void setNummber(String nummer) {
        this.nummer = nummer;
    }

    public String getNaam() {
        return naam;
    }

    public void setNaam(String naam) {
        this.naam = naam;
    }

    @Override
    public String toString() {
        return naam+" - "+nummer+" - "+" Saldo: "+saldo;
    }
}
