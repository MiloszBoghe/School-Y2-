using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace CocktailBarData
{
    public static class CocktailIngredientsRepository
    {
        public static List<CocktailIngredient> GetCocktailIngredients(int cocktailId)
        {
            // DONE: gebruik de nodige ADO.NET classes om 
            // de gegevens uit de database te lezen
            // Schrijf een SELECT query om de gegevens van de
            // CocktailIngredients tabel te lezen, samen met 
            // de naam en de eenheid van het ingredient (2 kolommen uit de Ingredients tabel)
            // Tip: null can je casten naar een nullable decimal (decimal?) als volgt: (decimal?) null

            SqlDataReader reader = null;
            List<CocktailIngredient> cocktailIngredients = new List<CocktailIngredient>();
            SqlConnection connection = ConnectionFactory.GetConnection();

            string selectStatement =
                "SELECT ci.CocktailId, ci.IngredientId, i.Name, ci.Quantity, i.unit " +
                "FROM CocktailIngredients as ci " +
                "INNER JOIN Ingredients as i " +
                "ON ci.IngredientId=i.Id " +
                "WHERE ci.CocktailId = @Cid ";

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.Parameters.AddWithValue("@Cid", cocktailId);
            command.CommandText = selectStatement;

            try
            {
                connection.Open();
                reader = command.ExecuteReader();

                int ingredientIdOrdinal = reader.GetOrdinal("IngredientId");
                int nameOrdinal = reader.GetOrdinal("Name");
                int quantityOrdinal = reader.GetOrdinal("Quantity");
                int unitOrdinal = reader.GetOrdinal("Unit");

                while (reader.Read())
                {
                    CocktailIngredient cocktailIngredient = new CocktailIngredient()
                    {
                        CocktailId = cocktailId,
                        IngredientId = reader.GetInt32(ingredientIdOrdinal),
                        IngredientName = reader.IsDBNull(nameOrdinal) ? null : reader.GetString(nameOrdinal),
                        Quantity = reader.IsDBNull(quantityOrdinal) ? (decimal?)null : reader.GetDecimal(quantityOrdinal),
                        Unit = reader.IsDBNull(unitOrdinal) ? null : reader.GetString(unitOrdinal)
                    };
                    cocktailIngredients.Add(cocktailIngredient);
                }
            }
            finally
            {
                reader?.Close();
                connection?.Close();
            }

            return cocktailIngredients;
        }
    }
}
