using System.Collections.Generic;
using System.Data.SqlClient;

namespace InvoiceQueries.Data
{
    public static class VendorRepository
    {
        public static IList<Vendor> GetAll()
        {
            List<Vendor> vendors = new List<Vendor>();

            string selectStatement =
                "SELECT VendorID, Name FROM Vendors";

            SqlConnection connection = ConnectionFactory.CreatePayablesConnection();
            SqlDataReader reader = null;
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);

            try
            {
                connection.Open();
                reader = selectCommand.ExecuteReader();
                int vendorIdOrdinal = reader.GetOrdinal("VendorID");
                int nameOrdinal = reader.GetOrdinal("Name");

                while (reader.Read())
                {
                    var vendor = new Vendor
                    {
                        VendorId = reader.GetInt32(vendorIdOrdinal),
                        Name = reader.GetString(nameOrdinal)
                    };
                    vendors.Add(vendor);
                }
            }
            finally
            {
                reader?.Close();
                connection?.Close();
            }
            return vendors;
        }
    }
}
