using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1
{
    public class Game : INotifyPropertyChanged
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    
}
