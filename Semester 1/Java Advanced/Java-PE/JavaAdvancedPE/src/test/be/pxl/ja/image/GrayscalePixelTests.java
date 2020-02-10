package be.pxl.ja.image;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class GrayscalePixelTests {
    private GrayscalePixel pixel = new GrayscalePixel(120);
    private GrayscalePixel pixel2 = new GrayscalePixel(150);

    @Test
    public void DistanceShouldBeGreaterThanZero() {
        Assertions.assertTrue(pixel.Distance(pixel2) > 0);
        Assertions.assertTrue(pixel2.Distance(pixel) > 0);
    }

    @Test
    public void DistanceResultIsCorrect() {
        Assertions.assertEquals(pixel.Distance(pixel2),30);
        Assertions.assertEquals(pixel2.Distance(pixel),30);
    }
}
