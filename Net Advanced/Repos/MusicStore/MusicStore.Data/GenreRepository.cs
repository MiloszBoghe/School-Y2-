using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Data
{
    public static class GenreRepository
    {
        public static IList<Genre> GetGenres()
        {
            SqlDataReader reader = null;
            IList<Genre> genreList = new List<Genre>();
            SqlConnection connection = ConnectionFactory.CreateSqlConnection();

            string selectStatement =
                "select InvoiceNumber, InvoiceDate, InvoiceTotal, PaymentTotal,CreditTotal,DueDate " +
                "From Invoices " +
                "where InvoiceTotal - PaymentTotal - CreditTotal > 0 " +
                "Order By DueDate";

            SqlCommand command = new SqlCommand(selectStatement, connection);

            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                int invoiceNumberOrder = reader.GetOrdinal("InvoiceNumber");
                
                while (reader.Read())
                {
                    //string invoiceNumber = reader["InvoiceNumber"].ToString();
                    Genre genre = new Genre()
                    {

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
