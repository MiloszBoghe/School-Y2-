using System.Collections.Generic;
using System.Data.SqlClient;

namespace PayablesData
{
    public static class GLAccountRepository
    {
        public static IList<GLAccount> GetAll()
        {        
            var accountList = new List<GLAccount>();

            string selectStatement =
                "SELECT AccountNo, Description " +
                "FROM GLAccounts " +
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
                    GLAccount account = new GLAccount
                    {
                        AccountNo = (int) reader["AccountNo"],
                        Description = reader["Description"].ToString()
                    };
                    accountList.Add(account);
                }
                
            }
            finally
            {
                reader?.Close();
                connection?.Close();
            }

            return accountList;
        }
    }
}
