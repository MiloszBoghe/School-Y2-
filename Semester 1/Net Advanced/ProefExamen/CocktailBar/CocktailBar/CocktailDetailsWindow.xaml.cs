using CocktailBarData;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CocktailBar
{
    /// <summary>
    /// Interaction logic for CocktailDetails.xaml
    /// </summary>
    public partial class CocktailDetailsWindow : Window
    {
        // TODO: Voeg de nodige code toe: zorg dat de details
        // van de cocktail, in dit venster worden getoond.
        // Zorg dat de ingrediënten van de cocktail in een listview worden getoond.
        public CocktailDetailsWindow(Cocktail cocktail)
        {
            DataContext = cocktail;
            InitializeComponent();
            IngredientsListView.ItemsSource = CocktailIngredientsRepository.GetCocktailIngredients(cocktail.Id);
            IngredientsListView.DataContext = IngredientsListView.ItemsSource;
        }

    }
}
