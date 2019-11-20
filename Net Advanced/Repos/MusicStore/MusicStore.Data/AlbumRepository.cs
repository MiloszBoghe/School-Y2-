using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace MusicStore.Data
{
    public static class AlbumRepository
    {
        public static ObservableCollection<Album> GetAlbumsByGenre(int id)
        {
            SqlDataReader reader = null;
            ObservableCollection<Album> albumList = new ObservableCollection<Album>();
            SqlConnection connection = ConnectionFactory.CreateSqlConnection();

            string selectStatement =
                "select albumid, title " +
                "From album " +
                "where genreId =  " + id;

            SqlCommand command = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                int genreId = reader.GetOrdinal("GenreId");
                int title = reader.GetOrdinal("Title");
                int artistId = reader.GetOrdinal("ArtistId");
                while (reader.Read())
                {
                    Album album = new Album()
                    {
                        GenreId = reader.GetInt32(genreId),
                        Title = reader.GetString(title),
                        ArtistId = reader.GetInt32(artistId)
                    };
                    albumList.Add(album);
                }
            }
            finally
            {
                reader?.Close();
                connection?.Close();
            }
            return albumList;
        }
    }
}
