using MusicStore.Data;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace MusicStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Genre> genreList { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                genreList = GenreRepository.GetGenres();
                comboBox.DataContext = genreList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

        }
    }
}
