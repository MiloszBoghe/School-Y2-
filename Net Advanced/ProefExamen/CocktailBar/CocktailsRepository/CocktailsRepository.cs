using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace CocktailBarData
{
    public class CocktailsRepository
    {
        public static List<Cocktail> GetAllCocktails()
        {
            // DONE: Gebruik de nodige ADO.NET classes om alle cocktails uit de database te lezen.
            //init
            SqlDataReader reader = null;
            List<Cocktail> cocktails = new List<Cocktail>();
            SqlConnection connection = ConnectionFactory.GetConnection();

            //select:
            string selectStatement =
                "select * " +
                "From cocktails ";

            //command:
            SqlCommand command = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Cocktail cocktail = new Cocktail()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = Convert.ToString(reader["Name"]),
                        Description = Convert.ToString(reader["Description"]),
                        Instructions = Convert.ToString(reader["Instructions"])
                    };
                    cocktails.Add(cocktail);
                }
            }
            finally
            {
                reader?.Close();
                connection?.Close();
            }
            return cocktails;
        }
    }
}

