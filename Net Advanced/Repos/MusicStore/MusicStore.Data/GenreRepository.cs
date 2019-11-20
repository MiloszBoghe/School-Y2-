using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace MusicStore.Data
{
    public static class GenreRepository
    {
        public static ObservableCollection<Genre> GetGenres()
        {
            SqlDataReader reader = null;
            ObservableCollection<Genre> genreList = new ObservableCollection<Genre>();
            SqlConnection connection = ConnectionFactory.CreateSqlConnection();

            string selectStatement =
                "select genreid, name, description " +
                "From Genre ";

            SqlCommand command = new SqlCommand(selectStatement, connection);

            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                int genreId = reader.GetOrdinal("GenreId");
                int genreName = reader.GetOrdinal("Name");
                int genreDesc = reader.GetOrdinal("Description");

                while (reader.Read())
                {
                    Genre genre = new Genre()
                    {
                        GenreId = reader.GetInt32(genreId),
                        Name = reader.GetString(genreName),
                        Description = reader.GetString(genreDesc)
                    };
                    genreList.Add(genre);
                }
            }
            finally
            {
                reader?.Close();
                connection?.Close();
            }
            return genreList;
        }
    }
}
