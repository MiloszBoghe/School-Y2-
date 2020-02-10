package be.pxl.ja.Week1.unittesting;

import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.assertEquals;

public class PersoonTest {
    @Test
    public void testGetVolledigeNaam(){
        Persoon p = new Persoon("Milosz","Boghe");
        assertEquals("Milosz Boghe",p.getVolledigeNaam());
    }
    @Test
    public void testGetVolledigeNaamVoornaamNull(){
        Persoon p = new Persoon(null,"Boghe");
        assertEquals("? Boghe",p.getVolledigeNaam());
    }
    @Test
    public void testGetVolledigeNaamNaamNull(){
        Persoon p = new Persoon("Milosz","?");
        assertEquals("Milosz ?",p.getVolledigeNaam());
    }
}
