package be.pxl.ja.image;

import javax.imageio.ImageIO;
import java.awt.image.BufferedImage;
import java.io.IOException;
import java.nio.file.Path;
import java.util.List;

public class ImageWriter {

	private static final String FORMAT = "jpg";

	public static <T extends PixelToInt> void writeImage(Path path, List<List<T>> pixels) throws IOException {
		BufferedImage image = new BufferedImage(pixels.get(0).size(), pixels.size(), BufferedImage.TYPE_INT_RGB);
		int rowIndex = 0;
		for (List<T> row : pixels) {
			int colIndex = 0;
			for (T col : row) {
				image.setRGB(colIndex, rowIndex, col.toRGB());
				colIndex++;
			}
			rowIndex++;
		}
		ImageIO.write(image, FORMAT, path.toFile());
	}
}
