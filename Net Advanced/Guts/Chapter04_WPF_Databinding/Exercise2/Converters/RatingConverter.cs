using System;
using System.Globalization;
using System.Windows.Data;

namespace Exercise2.Converters
{
    public class RatingConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double rating = (double)value;
            return rating * 10;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int rating = (int)value;
            return rating / 10.0;
        }
    }
}
