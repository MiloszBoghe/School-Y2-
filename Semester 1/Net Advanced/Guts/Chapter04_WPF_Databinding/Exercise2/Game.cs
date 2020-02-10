using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Exercise2
{
    public class Game : INotifyPropertyChanged
    {
        private double rating;

        public int GameId { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public double Rating { get=>rating; set {
                rating = value;
                OnPropertyChanged(nameof(Rating));
            } }
        public bool IsUnder18 { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
