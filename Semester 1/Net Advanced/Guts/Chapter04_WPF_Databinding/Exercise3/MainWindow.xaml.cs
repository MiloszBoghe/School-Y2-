using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Exercise3
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Game> games;

        public ObservableCollection<Game> Games { get => games; set => games = value; }

        public MainWindow()
        {
            InitializeComponent();
            GamesListView.ItemsSource = GetDummyGames();
            DataContext = new Game();

        }

        private IList<Game> GetDummyGames()
        {
            games = new ObservableCollection<Game>
            {
                new Game
                {
                    Name = "GTA V",
                    Description =
                        "Dit is een spel waarbij een speler allerlei handelingen kan doen zoals rennen, zwemmen, autorijden om het spel te navigeren.Hoe kan je de game uitspelen ? Door alle missies te halen en niet gepakt te worden door politie.",
                },
                new Game
                {
                    Name = "Call of Duty:Infinite Warfare",
                    Description =
                        "Het is een game waarbij je missies uitvoert en heeft een zombiemodus. Je speelt dit spel op veel verschillende plekken en schiet vaak vanuit de cockpit in plaats van tijdens het lopen.",
                }
            };
            return games;
        }

        private void GamesListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            DataContext = (Game)GamesListView.SelectedItem;
        }

        private void AddNewGameButton_Click(object sender, RoutedEventArgs e)
        {
            Game game = NewGameGroupBox.DataContext as Game;
            if (game.Name == "" || game.Name is null || game.Description == "" || game.Description is null)
            {
                ErrorMessageTextBlock.Text = "A game with an empty name cannot be added";
            }
            else
            {
                Games.Add(game);
                DataContext = new Game();
                ErrorMessageTextBlock.Text = "";
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
