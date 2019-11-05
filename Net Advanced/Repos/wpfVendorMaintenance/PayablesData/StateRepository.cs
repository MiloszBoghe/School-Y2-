using System.Collections.Generic;
using System.Data.SqlClient;

namespace PayablesData
{
    public static class StateRepository
    {
        public static IList<State> GetAll()
        {
            var states = new List<State>();

            string selectStatement =
                "SELECT StateCode, StateName, FirstZipCode, LastZipCode " +
                "FROM States " +
                "ORDER BY StateName";
            SqlConnection connection = ConnectionFactory.CreatePayablesConnection();
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);

            SqlDataReader reader = null;
            try
            {
                connection.Open();
                reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    State state = new State
                    {
                        StateCode = reader["StateCode"].ToString(),
                        StateName = reader["StateName"].ToString(),
                        FirstZipCode = (int) reader["FirstZipCode"],
                        LastZipCode = (int) reader["LastZipCode"]
                    };
                    states.Add(state);
                }         
            }
            finally
            {
                reader?.Close();
                connection?.Close();
            }
            return states;
        }
    }
}
