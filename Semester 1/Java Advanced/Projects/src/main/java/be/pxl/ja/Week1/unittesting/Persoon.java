package be.pxl.ja.Week1.unittesting;

public class Persoon {
    private String voornaam;
    private String naam;

    public Persoon(String voornaam, String naam) {
        this.voornaam = voornaam;
        this.naam = naam;
    }

    public String getVolledigeNaam() {
        return (this.voornaam == null ? "?" : this.voornaam) + " " + (this.naam == null ? "?" : this.naam);
    }
}
