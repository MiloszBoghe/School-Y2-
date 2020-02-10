using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicesDueSolution.Data
{
    public static class InvoiceRepository
    {
        public static IList<Invoice> GetInvoiceDue()
        {
            SqlDataReader reader = null;
            IList<Invoice> invoiceList = new List<Invoice>();
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
                int invoiceDateOrder = reader.GetOrdinal("InvoiceDate");
                int invoiceTotalOrder = reader.GetOrdinal("InvoiceTotal");
                int paymentTotalOrder = reader.GetOrdinal("PaymentTotal");
                int creditTotalOrder = reader.GetOrdinal("CreditTotal");
                int dueDateOrder = reader.GetOrdinal("DueDate");
                while (reader.Read())
                {
                    //string invoiceNumber = reader["InvoiceNumber"].ToString();
                    Invoice invoice = new Invoice()
                    {
                        InvoiceNumber = reader.GetString(invoiceNumberOrder),
                        InvoiceDate = reader.GetDateTime(invoiceDateOrder),
                        InvoiceTotal = reader.GetDecimal(invoiceTotalOrder),
                        PaymentTotal = reader.GetDecimal(paymentTotalOrder),
                        CreditTotal = reader.GetDecimal(creditTotalOrder),
                        DueDate = reader.GetDateTime(dueDateOrder)
                    };
                    invoiceList.Add(invoice);
                }
            }
            finally
            {
                reader?.Close();
                connection?.Close();
            }
            return invoiceList;
        }
    }
}
