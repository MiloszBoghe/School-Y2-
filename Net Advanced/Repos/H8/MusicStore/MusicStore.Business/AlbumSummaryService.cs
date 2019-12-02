using MusicStore.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Business
{
    public class AlbumSummaryService
    {
        public static ObservableCollection<Album> GetAlbumSummariesByGenre(int id)
        {
            ObservableCollection<Album> albumSummary = AlbumRepository.GetAlbumsByGenre(id);
            foreach(Album a in albumSummary)
            {
                a.ArtistName = ArtistRepository.GetArtist(a.ArtistId).Name;
            }
            return albumSummary;
        }
    }
}
