using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Data
{
    public static class AlbumRepository
    {
        public static IList<Album> GetAlbumsByGenre()
        {
            SqlDataReader reader = null;
            IList<Album> albumList = new List<Album>();
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
                    Album album = new Album()
                    {

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
