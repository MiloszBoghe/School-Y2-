using System.Configuration;
using System.Data.SqlClient;
using Lottery.Data.Interfaces;

namespace Lottery.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        public SqlConnection CreateSqlConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LotteryConnection"].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}
