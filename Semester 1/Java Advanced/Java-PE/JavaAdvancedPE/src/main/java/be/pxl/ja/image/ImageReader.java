package be.pxl.ja.image;

import javax.imageio.ImageIO;
import java.awt.image.BufferedImage;
import java.io.IOException;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.List;

public class ImageReader {
	private static List<List<RGBPixel>> createPixelList(BufferedImage image) {
		int width = image.getWidth();
		int height = image.getHeight();

		List<List<RGBPixel>> pixels = new ArrayList<>();
		for (int i = 0; i < height; i++) {
			List<RGBPixel> row = new ArrayList<>();
			for (int j = 0; j < width; j++) {
				int pixel = image.getRGB(j, i);
				int alpha = (pixel >> 24) & 0xff;
				int red = (pixel >> 16) & 0xff;
				int green = (pixel >> 8) & 0xff;
				int blue = (pixel) & 0xff;
				row.add(new RGBPixel(red, green, blue));
			}
			pixels.add(row);
		}
		return pixels;
	}

	public static List<List<RGBPixel>> readImage(Path path) {
		try {
			BufferedImage image = ImageIO.read(path.toFile());
			return createPixelList(image);
		} catch (IOException e) {
			System.err.println(e.getMessage());
		}
		return null;
	}
}
