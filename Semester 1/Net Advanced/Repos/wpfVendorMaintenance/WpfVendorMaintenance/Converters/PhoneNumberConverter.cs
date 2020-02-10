using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfVendorMaintenance.Converters
{
    public class PhoneNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string phone = (string) value;
            return string.IsNullOrEmpty(phone) ? "" : FormatPhoneNumber(phone);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string formatedPhone = (string)value ?? string.Empty;
            return formatedPhone.Replace(".", "");
        }

        private string FormatPhoneNumber(string phone)
        {
            return phone.Substring(0, 3) + "." +
                   phone.Substring(3, 3) + "." +
                   phone.Substring(6, 4);
        }
    }
}
