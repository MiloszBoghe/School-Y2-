using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicesDueSolution.Data
{
    internal static class ConnectionFactory
    {
        public static SqlConnection CreateSqlConnection()
        {

            //Methode1
            //string connectionString =
            //    "Data Source={localdb}" +
            //    "\\MSSqllocalDb;" +
            //    "Initial Catalog=Payables;" +
            //    "Integrated Security=True";

            //SqlConnectionStringBuilder connBuilder = new SqlConnectionStringBuilder()
            //{
            //    DataSource = "(localdb)\\MSSqllocalDb",
            //    InitialCatalog = "Payables",
            //    IntegratedSecurity = true
            //};
            //return new SqlConnection(connBuilder.ConnectionString);

            string connectionString = ConfigurationManager.ConnectionStrings["PayablesConnectionString"].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}
