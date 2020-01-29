using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Exercise6
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            numberTextBox.Text += ((Button)e.Source).Content;
        }

        private void NumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = e.Text.Length != 1 || !Char.IsDigit(Convert.ToChar(e.Text));
        }
    }
}