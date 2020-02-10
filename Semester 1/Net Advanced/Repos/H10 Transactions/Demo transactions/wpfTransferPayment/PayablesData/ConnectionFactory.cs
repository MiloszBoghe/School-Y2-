using System.Data.SqlClient;
using System.Configuration;

namespace PayablesData
{
    public static class ConnectionFactory
    {
        public static SqlConnection CreatePayablesDbConnection()
        {
            string connectionString =  ConfigurationManager.ConnectionStrings["PayablesConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
