using System.Data.SqlClient;

namespace PayablesData
{
    internal static class ConnectionFactory
    {
        public static SqlConnection CreatePayablesConnection()
        {
            string connectionString =
                "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Payables;" +
                "Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
