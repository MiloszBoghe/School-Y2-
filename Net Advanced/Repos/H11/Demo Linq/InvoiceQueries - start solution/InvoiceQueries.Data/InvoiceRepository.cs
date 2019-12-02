using System.Collections.Generic;
using System.Data.SqlClient;

namespace InvoiceQueries.Data
{
    public static class InvoiceRepository
    {
        public static IList<Invoice> GetAll()
        {
            List<Invoice> invoices = new List<Invoice>();

            string selectStatement =
                "SELECT InvoiceNumber, InvoiceDate, InvoiceTotal, " +
                "PaymentTotal, CreditTotal, DueDate, VendorID " +
                "FROM Invoices";

            SqlConnection connection = ConnectionFactory.CreatePayablesConnection();
            SqlDataReader reader = null;
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);

            try
            {
                connection.Open();
                reader = selectCommand.ExecuteReader();
                int invoiceNumberOrdinal = reader.GetOrdinal("InvoiceNumber");
                int invoiceDateOrdinal = reader.GetOrdinal("InvoiceDate");
                int invoiceTotalOrdinal = reader.GetOrdinal("InvoiceTotal");
                int paymentTotalOrdinal = reader.GetOrdinal("PaymentTotal");
                int creditTotalOrdinal = reader.GetOrdinal("CreditTotal");
                int dueDateOrdinal = reader.GetOrdinal("DueDate");
                int vendorIdOrdinal = reader.GetOrdinal("VendorID");
                while (reader.Read())
                {
                    var invoice = new Invoice
                    {
                        InvoiceNumber = reader.GetString(invoiceNumberOrdinal),
                        InvoiceDate = reader.GetDateTime(invoiceDateOrdinal),
                        InvoiceTotal = reader.GetDecimal(invoiceTotalOrdinal),
                        PaymentTotal = reader.GetDecimal(paymentTotalOrdinal),
                        CreditTotal = reader.GetDecimal(creditTotalOrdinal),
                        DueDate = reader.GetDateTime(dueDateOrdinal),
                        VendorId = reader.GetInt32(vendorIdOrdinal)
                    };
                    invoices.Add(invoice);
                }
            }
            finally
            {
                reader?.Close();
                connection?.Close();
            }
            return invoices;
        }
    }
}
