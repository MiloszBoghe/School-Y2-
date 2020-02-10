using CocktailBarData;
using System;
using System.Windows;

namespace CocktailBar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //DONE Voeg hier de nodige code toe: 
        // Bij het dubbelklikken op een cocktail moet er naar 
        // het CocktailDetailsWindow genavigeerd worden.
        // Wanneer er op de + wordt geklikt, moet er naar het 
        // AddCocktailWindow genavigeerd worden
        public MainWindow()
        {
            InitializeComponent();
            cocktailstListView.ItemsSource = CocktailsRepository.GetAllCocktails();
            DataContext = cocktailstListView.ItemsSource;
        }

        private void CocktailstListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Window cocktailDetailsWindow = new CocktailDetailsWindow((Cocktail)cocktailstListView.SelectedItem);
            cocktailDetailsWindow.Show();
        }
    }
}
