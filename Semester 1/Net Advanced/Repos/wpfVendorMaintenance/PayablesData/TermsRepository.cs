using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PayablesData
{
    public static class TermsRepository
    {
        public static List<Terms> GetAll()
        { 
            var allTerms = new List<Terms>();

            string selectStatement =
                "SELECT TermsID, Description, DueDays " +
                "FROM Terms " +
                "ORDER BY Description";
            SqlConnection connection = ConnectionFactory.CreatePayablesConnection();
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);

            SqlDataReader reader = null;
            try
            {
                connection.Open();
                reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    Terms terms = new Terms
                    {
                        TermsId = Convert.ToInt32(reader["TermsID"]),
                        Description = reader["Description"].ToString(),
                        DueDays = Convert.ToInt32(reader["DueDays"])
                    };
                    allTerms.Add(terms);
                }
                
            }
            finally
            {
                reader?.Close();
                connection?.Close();
            }

            return allTerms;
        }
    }
}
