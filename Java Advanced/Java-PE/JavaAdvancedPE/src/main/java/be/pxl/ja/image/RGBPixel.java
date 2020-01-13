package be.pxl.ja.image;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Stream;

public class RGBPixel implements PixelToInt {
    private int red;
    private int green;
    private int blue;

    public RGBPixel(int red, int green, int blue) {
        this.red = red;
        this.green = green;
        this.blue = blue;
    }


    List<Integer> convertToGrayscale() {
        //Een list maken met RGB als integers.
        int avg = (red + blue + green) / 3;
        return Arrays.asList(avg, avg, avg);
    }

    public int toRGB() {
        int rgb = red;
        rgb = (rgb << 8) + green;
        rgb = (rgb << 8) + blue;
        return rgb;
    }

    public GrayscalePixel convertToGrayScale() {
        int avg = (this.red + this.green + this.blue) / 3;
        return new GrayscalePixel(avg);
    }

    @Override
    public String toString() {
        return "(" + red + ", " + green + ", " + blue + ")";
    }

}
