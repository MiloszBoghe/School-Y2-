package be.pxl.ja.image;

import be.pxl.ja.common.DistanceUtil;

import java.io.IOException;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.*;
import java.util.List;
import java.util.stream.Collectors;

public class ImageArt {

    public static void main(String[] args) throws IOException {
        /*
        RGBPixel prussianBlue = new RGBPixel(0, 48, 80);
        RGBPixel desaturatedCyan = new RGBPixel(112, 150, 160);
        RGBPixel peachYellow = new RGBPixel(250, 227, 173);
        RGBPixel lava = new RGBPixel(218, 20, 21);
         */
        RGBPixel prussianBlue = new RGBPixel(102, 0, 0);
        RGBPixel desaturatedCyan = new RGBPixel(255, 102, 178);
        RGBPixel lava = new RGBPixel(204, 0, 0);
        RGBPixel peachYellow = new RGBPixel(255, 153, 204);
        List<RGBPixel> faireyColors = Arrays.asList(prussianBlue, lava, desaturatedCyan, peachYellow);

        Path resources = Paths.get(System.getProperty("user.dir")).resolve("src/main/resources");

        //Change this to change the image
        Path path = resources.resolve("test.jpg");

        List<List<RGBPixel>> imagePixels = ImageReader.readImage(path);

        //GrayScale: make Grayscale list  --> make image
        List<List<GrayscalePixel>> veryGrayPixels = imagePixels.stream().map(list -> {
            return list.stream().map(RGBPixel::convertToGrayScale).collect(Collectors.toList());
        }).collect(Collectors.toList());
        ImageWriter.writeImage(resources.resolve("grayResult.jpg"), veryGrayPixels);


        //Fairey : Make TreeSet --> make TranslationMap --> make Fairey image --> ????? --> Profit
        TreeSet<GrayscalePixel> sortedGrayPixels = veryGrayPixels.stream().flatMap(Collection::stream).collect(Collectors.toCollection(TreeSet::new));
        Map<GrayscalePixel, RGBPixel> translationMap = createTranslationMap(faireyColors, sortedGrayPixels);

        System.out.println(translationMap);

        List<List<RGBPixel>> faireyPixels = veryGrayPixels.stream().map(list -> list.stream()
                .map(pixel -> translationMap.get(DistanceUtil.findClosest(translationMap.keySet(), pixel)))
                .collect(Collectors.toList())).collect(Collectors.toList());

        ImageWriter.writeImage(resources.resolve("faireyResult.jpg"), faireyPixels);
    }


    private static Map<GrayscalePixel, RGBPixel> createTranslationMap(List<RGBPixel> faireyColors, TreeSet<GrayscalePixel> allGreyscalePixels) {
        int size = allGreyscalePixels.size() / faireyColors.size();

        Map<GrayscalePixel, RGBPixel> translationMap = new HashMap<>();
        Iterator<GrayscalePixel> iterator = allGreyscalePixels.iterator();
        int startIndex = size / 2;
        List<Integer> preferedIndeces = new ArrayList<>();
        for (int group = 0; group < faireyColors.size(); group++) {
            preferedIndeces.add(startIndex);
            startIndex += size;
        }
        int index = 0;
        while (iterator.hasNext()) {
            GrayscalePixel grayscalePixel = iterator.next();
            if (preferedIndeces.contains(index)) {
                int position = preferedIndeces.indexOf(index);
                translationMap.put(grayscalePixel, faireyColors.get(position));
            }
            index++;
        }
        return translationMap;
    }

}
