using System;
using System.Data;
using System.Data.SqlClient;

namespace PayablesData
{
    public static class VendorRepository
    {
        public static Vendor GetById(int vendorId)
        {
            Vendor vendor = null;

            string selectStatement =
                "SELECT VendorID, Name, Address1, Address2, City, State, " +
                    "ZipCode, Phone, ContactFName, ContactLName, " +
                    "DefaultAccountNo, DefaultTermsID " +
                "FROM Vendors " +
                "WHERE VendorID = @VendorId";
            SqlConnection connection = ConnectionFactory.CreatePayablesConnection();
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@VendorId", vendorId);

            SqlDataReader reader = null;
            try
            {
                connection.Open();
                reader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    vendor = new Vendor
                    {
                        VendorId = (int)reader["VendorID"],
                        Name = reader["Name"].ToString(),
                        Address1 = reader["Address1"].ToString(),
                        Address2 = reader["Address2"].ToString(),
                        City = reader["City"].ToString(),
                        State = reader["State"].ToString(),
                        ZipCode = reader["ZipCode"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        ContactLName = reader["ContactLName"].ToString(),
                        ContactFName = reader["ContactFName"].ToString(),
                        DefaultAccountNo = (int)reader["DefaultAccountNo"],
                        DefaultTermsId = (int)reader["DefaultTermsID"]
                    };
                }
            }
            finally
            {
                reader?.Close();
                connection?.Close();
            }

            return vendor;
        }

        /// <summary>
        /// Adds a vendor to the database
        /// </summary>
        /// <param name="vendor">The vendor to be added</param>
        /// <returns>The id of the inserted vendor</returns>
        public static int Add(Vendor vendor)
        {
            SqlConnection connection = ConnectionFactory.CreatePayablesConnection();
            SqlCommand insertCommand = CreateInsertCommand(vendor, connection);

            try
            {
                connection.Open();
                int createdVendorId = (int) insertCommand.ExecuteScalar();
                return createdVendorId;
            }
            finally
            {
                connection?.Close();
            }
        }

        public static bool Update(Vendor vendor)
        {           
            SqlConnection connection = ConnectionFactory.CreatePayablesConnection();
            var updateCommand = CreateUpdateCommand(vendor, connection);

            try
            {
                connection.Open();
                int numberOfRowsAffected = updateCommand.ExecuteNonQuery();
                return numberOfRowsAffected > 0;
            }
            finally
            {
                connection?.Close();
            }
        }

        private static SqlCommand CreateInsertCommand(Vendor vendor, SqlConnection connection)
        {
            string insertStatement =
                "INSERT Vendors " +
                "(Name, Address1, Address2, City, State, ZipCode, Phone, ContactFName, ContactLName, DefaultTermsID, DefaultAccountNo) " +
                "VALUES (@Name, @Address1, @Address2, @City, @State, @ZipCode, @Phone, @ContactFName, @ContactLName, @DefaultTermsID, @DefaultAccountNo) " +
                "; " + // We can execute 2 SQL queries by seperating them with a semicolon
                "SELECT SCOPE_IDENTITY()"; //Get the id of the inserted vendor (another option would be to use an output parameter. E.g. SET @VendorId = SCOPE_IDENTITY() )

            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@Name", vendor.Name);
            insertCommand.Parameters.AddWithValue("@Address1", vendor.Address1);
            if (vendor.Address2 == null)
            {
                insertCommand.Parameters.AddWithValue("@Address2", DBNull.Value);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Address2", vendor.Address2);
            }
            insertCommand.Parameters.AddWithValue("@City", vendor.City);
            insertCommand.Parameters.AddWithValue("@State", vendor.State);
            insertCommand.Parameters.AddWithValue("@ZipCode", vendor.ZipCode);
            if (vendor.Phone == null)
            {
                insertCommand.Parameters.AddWithValue("@Phone", DBNull.Value);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Phone", vendor.Phone);
            }
            if (vendor.ContactFName == null)
            {
                insertCommand.Parameters.AddWithValue("@ContactFName", DBNull.Value);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ContactFName", vendor.ContactFName);
            }
            if (vendor.ContactLName == null)
            {
                insertCommand.Parameters.AddWithValue("@ContactLName", DBNull.Value);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ContactLName", vendor.ContactLName);
            }
            insertCommand.Parameters.AddWithValue("@DefaultTermsID", vendor.DefaultTermsId);
            insertCommand.Parameters.AddWithValue("@DefaultAccountNo", vendor.DefaultAccountNo);
            return insertCommand;
        }

        private static SqlCommand CreateUpdateCommand(Vendor vendor, SqlConnection connection)
        {
            string updateStatement =
                "UPDATE Vendors SET " +
                "Name = @NewName, " +
                "Address1 = @NewAddress1, " +
                "Address2 = @NewAddress2, " +
                "City = @NewCity, " +
                "State = @NewState, " +
                "ZipCode = @NewZipCode, " +
                "Phone = @NewPhone, " +
                "ContactFName = @NewContactFName, " +
                "ContactLName = @NewContactLName, " +
                "DefaultTermsID = @NewDefaultTermsID, " +
                "DefaultAccountNo = @NewDefaultAccountNo " +
                "WHERE VendorID = @VendorID ";

            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);

            updateCommand.Parameters.AddWithValue("@NewName", vendor.Name);
            updateCommand.Parameters.AddWithValue("@NewAddress1", vendor.Address1);
            if (vendor.Address2 == "")
            {
                updateCommand.Parameters.AddWithValue("@NewAddress2", DBNull.Value);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewAddress2", vendor.Address2);
            }
            updateCommand.Parameters.AddWithValue("@NewCity", vendor.City);
            updateCommand.Parameters.AddWithValue("@NewState", vendor.State);
            updateCommand.Parameters.AddWithValue("@NewZipCode", vendor.ZipCode);
            if (vendor.Phone == "")
            {
                updateCommand.Parameters.AddWithValue("@NewPhone", DBNull.Value);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewPhone", vendor.Phone);
            }
            if (vendor.ContactFName == "")
            {
                updateCommand.Parameters.AddWithValue("@NewContactFName", DBNull.Value);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewContactFName", vendor.ContactFName);
            }
            if (vendor.ContactLName == "")
            {
                updateCommand.Parameters.AddWithValue("@NewContactLName", DBNull.Value);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewContactLName", vendor.ContactLName);
            }
            updateCommand.Parameters.AddWithValue("@NewDefaultTermsID", vendor.DefaultTermsId);
            updateCommand.Parameters.AddWithValue("@NewDefaultAccountNo", vendor.DefaultAccountNo);
            updateCommand.Parameters.AddWithValue("@VendorID", vendor.VendorId);

            return updateCommand;
        }
    }
}
