using System;
using System.Globalization;
using System.Windows.Data;

namespace CocktailBar.Converters
{
    public class ImageConverter : IValueConverter
    {
        // TODO: vul deze class aan: De naam van de image is de
        // naam van de cocktail zonder de spatie(s), met ".jpg" als extensie.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "Images/" + value.ToString().Replace(" ", "") + ".jpg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
