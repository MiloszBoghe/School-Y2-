using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CocktailBarData
{
    public class IngredientsRepository
    {
        public static List<Ingredient> GetAllIngredients()
        {
            // DONE: gebruik de nodige ADO classes om alle 
            // ingrediënten uit de database te lezen.
            SqlDataReader reader = null;
            List<Ingredient> ingredients = new List<Ingredient>();
            SqlConnection connection = ConnectionFactory.GetConnection();

            string selectStatement =
            "select * " +
            "From ingredients ";

			SqlCommand command = new SqlCommand(selectStatement, connection);
			try
			{
				connection.Open();
				reader = command.ExecuteReader();
				while (reader.Read())
				{
					Ingredient ingredient = new Ingredient()
					{
						Id = Convert.ToInt32(reader["Id"]),
						Name = Convert.ToString(reader["Name"]),
						Unit = Convert.ToString(reader["Unit"])
					};
					ingredients.Add(ingredient);
				}
			}
			finally
			{
				reader?.Close();
				connection?.Close();
			}

			return ingredients;
        }
    }
}

