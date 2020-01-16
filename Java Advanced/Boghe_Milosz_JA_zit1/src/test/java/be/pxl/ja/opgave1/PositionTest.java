package be.pxl.ja.opgave1;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

public class PositionTest {

    public static final String DATA_LINE = "26,209331,M. Salah,26,https://cdn.sofifa.org/players/4/19/209331.png,Egypt,https://cdn.sofifa.org/flags/111.png,88,89,Liverpool,https://cdn.sofifa.org/teams/2/light/9.png,€69.5M,€255K,2146,Left,3,3,4,High/ Medium,PLAYER_BODY_TYPE_25,Yes,RM,10,\"Jul 1, 2017\",,2023,5'9,157lbs,83+3,83+3,83+3,87+3,86+3,86+3,86+3,87+3,86+3,86+3,86+3,86+3,80+3,80+3,80+3,86+3,70+3,66+3,66+3,66+3,70+3,66+3,57+3,57+3,57+3,66+3,78,90,59,82,73,89,83,60,72,88,94,91,91,91,88,77,68,84,70,83,63,55,90,82,61,91,38,43,41,14,14,9,11,14,€137.3M";

    private String[] testData;
    private Position pos;

    @BeforeEach
    public void init() throws PositionFullException {
        pos = new Position(2);
        testData = DATA_LINE.split(",");
        pos.addPlayer(new Midfielder(testData));
    }

    @Test
    public void isFullReturnsFalseWhenNotFull() {
        Assertions.assertFalse(pos.isFull());
    }

    @Test
    public void isFullReturnsTrueWhenFull() throws PositionFullException {
        pos.addPlayer(new Defender(testData));
        Assertions.assertTrue(pos.isFull());
    }

    @Test
    public void testAddPlayerThrowsExceptionWhenFull() throws PositionFullException {
        if(pos.isFull()){
            Assertions.assertThrows(PositionFullException.class, () -> pos.addPlayer(new Defender(testData)));
        }
    }
}
