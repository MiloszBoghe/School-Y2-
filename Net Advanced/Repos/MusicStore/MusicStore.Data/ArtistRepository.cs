using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace MusicStore.Data
{
    public static class ArtistRepository
    {
        public static Artist GetArtist(int id)
        {
            SqlDataReader reader = null;
            Artist artist = null;
            SqlConnection connection = ConnectionFactory.CreateSqlConnection();

            string selectStatement =
                "select name,artistid " +
                "From Artist " +
                "where ArtistId = " + id;

            SqlCommand command = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                int artistId = reader.GetOrdinal("ArtistId");
                int artistName = reader.GetOrdinal("Name");
                while (reader.Read())
                {
                    artist = new Artist()
                    {
                        Name = reader.GetString(artistName),
                        ArtistId = reader.GetInt32(artistId)
                    };
                }
            }
            finally
            {
                reader?.Close();
                connection?.Close();
            }
            return artist;
        }
    }
}