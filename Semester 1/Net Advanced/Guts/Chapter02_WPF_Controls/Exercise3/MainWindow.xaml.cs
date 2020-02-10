using System;
using System.Windows;

namespace Exercise3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GrowButton_Click(object sender, RoutedEventArgs e)
        {
            if (oranje.Width < dikkeCanvas.Width-10)
            {
                oranje.Width += 10;
            }
        }

        private void ShrinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (oranje.Width >= 10)
            {
                oranje.Width -= 10;
            }
        }
    }
}
