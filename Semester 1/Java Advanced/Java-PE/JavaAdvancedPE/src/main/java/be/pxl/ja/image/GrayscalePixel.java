package be.pxl.ja.image;

import be.pxl.ja.common.DistanceFunction;

import java.awt.*;

public class GrayscalePixel implements PixelToInt, Comparable<GrayscalePixel>, DistanceFunction<GrayscalePixel> {
        private int grayscale;

        public GrayscalePixel(int grayscale) {
            this.grayscale = grayscale;
        }

        public int getGrayscale() {
            return grayscale;
        }

        @Override
        public int toRGB() {
            return new Color(grayscale, grayscale, grayscale).getRGB();
        }

        @Override
        public String toString() {
            return Integer.toString(grayscale);
        }

        @Override
        public double Distance(GrayscalePixel grayscalePixel) {
            return Math.abs(grayscale - grayscalePixel.grayscale);
        }

        @Override
        public int compareTo(GrayscalePixel grayscalePixel) {
            return Integer.compare(grayscale, grayscalePixel.grayscale);
        }
    }
