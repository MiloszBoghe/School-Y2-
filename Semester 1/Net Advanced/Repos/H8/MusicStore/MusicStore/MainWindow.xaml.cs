using MusicStore.Business;
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
        public ObservableCollection<Album> summary { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                genreList = GenreRepository.GetGenres();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void comboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            DataContext = summary;
            summary = AlbumSummaryService.GetAlbumSummariesByGenre(((Genre)comboBox.SelectedItem).GenreId);
            albumDataGrid.ItemsSource = summary;
        }
    }
}
