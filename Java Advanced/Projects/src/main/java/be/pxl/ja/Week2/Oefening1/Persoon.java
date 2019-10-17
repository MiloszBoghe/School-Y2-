package be.pxl.ja.Week2.Oefening1;

import java.time.LocalDate;
import java.time.temporal.ChronoUnit;

public class Persoon {
    private String naam;
    private LocalDate geboorteDatum;

    public Persoon(String naam, int dag, int maand, int jaar) {
        this.naam = naam;
        geboorteDatum = LocalDate.of(jaar, maand, dag);
    }

    public int aantalDagenTotVerjaardag() {
        int dayOfYear = LocalDate.now().getDayOfYear();
        int birthDay = geboorteDatum.getDayOfYear();
        if (dayOfYear > birthDay) {
            return geboorteDatum.getYear();
        }else{
            return birthDay - dayOfYear;
        }
    }

    public String getNaam() {
        return naam;
    }

}
