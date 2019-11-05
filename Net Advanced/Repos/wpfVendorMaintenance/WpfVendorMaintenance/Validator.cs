using System;
using System.Windows.Controls;

namespace WpfVendorMaintenance
{
    internal class Validator
    {
        public static string Title { get; set; } = "Entry Error";

        public static bool HasValue(Control control)
        {
            switch (control.GetType().ToString())
            {
                case "System.Windows.Forms.TextBox":
                    TextBox textBox = (TextBox)control;
                    return textBox.Text != "";
                case "System.Windows.Forms.ComboBox":
                    ComboBox comboBox = (ComboBox)control;
                    return comboBox.SelectedIndex != -1;
            }
            return true;
        }

        public static bool IsDecimal(TextBox textBox)
        {
            try
            {
                Convert.ToDecimal(textBox.Text);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsInt32(TextBox textBox)
        {
            try
            {
                int.Parse(textBox.Text);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsStateZipCode(TextBox textBox, int firstZip, int lastZip)
        {
            int zipCode = Convert.ToInt32(textBox.Text);
            if (zipCode <= firstZip || zipCode >= lastZip)
            {
                return false;
            }
            return true;
        }

        public static bool IsPhoneNumber(TextBox textBox)
        {
            string phoneChars = textBox.Text.Replace(".", "");
            try
            {
                long.Parse(phoneChars);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
