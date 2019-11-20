using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace MusicStore.Data
{
    public static class ArtistRepository
    {
        public static ObservableCollection<Artist> GetArtist(int id)
        {
            SqlDataReader reader = null;
            ObservableCollection<Artist> artistList = new ObservableCollection<Artist>();
            SqlConnection connection = ConnectionFactory.CreateSqlConnection();

            string selectStatement =
                "select name " +
                "From artist " +
                "where artistid = " + id;

            SqlCommand command = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                int artistId = reader.GetOrdinal("ArtistId");
                int artistName = reader.GetOrdinal("Name");
                while (reader.Read())
                {
                    Artist artist = new Artist()
                    {
                        ArtistId = reader.GetInt32(artistId),
                        Name = reader.GetString(artistName)
                    };
                    artistList.Add(artist);
                }
            }
            finally
            {
                reader?.Close();
                connection?.Close();
            }
            return artistList;
        }
    }
}