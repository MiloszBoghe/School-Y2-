using System.Configuration;
using System.Data.SqlClient;

namespace CocktailBarData
{
    public class ConnectionFactory
    {
        public static SqlConnection GetConnection()
        {
            // DONE: Lees de connectionstring uit de configfile
            string connectionString = ConfigurationManager.ConnectionStrings["CocktailsConnectionString"].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}
