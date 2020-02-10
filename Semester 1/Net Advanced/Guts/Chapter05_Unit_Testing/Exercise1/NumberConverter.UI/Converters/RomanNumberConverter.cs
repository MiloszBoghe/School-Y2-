using System;
using System.Globalization;
using System.Windows.Data;

namespace NumberConverter.UI.Converters
{
    public class RomanNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string)) throw new ArgumentException("Not a string");

            bool tryParse = int.TryParse((string)value, out int number);

            if (!tryParse) return "Invalid number";
            return (number <= 0) || (number > 3999) ? "Out of Roman range (1-3999)" : ConvertToRoman(number);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }

        public string ConvertToRoman(int n)
        {
            return n >= 1000 ? "M" + ConvertToRoman(n - 1000) : n >= 900 ? "CM" + ConvertToRoman(n - 900) :
                   n >= 500 ? "D" + ConvertToRoman(n - 500) : n >= 400 ? "CD" + ConvertToRoman(n - 400) :
                   n >= 100 ? "C" + ConvertToRoman(n - 100) : n >= 90 ? "XC" + ConvertToRoman(n - 90) :
                   n >= 50 ? "L" + ConvertToRoman(n - 50) : n >= 40 ? "XL" + ConvertToRoman(n - 40) :
                   n >= 10 ? "X" + ConvertToRoman(n - 10) : n >= 9 ? "IX" + ConvertToRoman(n - 9) :
                   n >= 5 ? "V" + ConvertToRoman(n - 5) : n >= 4 ? "IV" + ConvertToRoman(n - 4) :
                   n >= 1 ? "I" + ConvertToRoman(n - 1) : "";
        }
    }
}
