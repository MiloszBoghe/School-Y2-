using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Lottery.Data.Interfaces;
using Lottery.Domain;

namespace Lottery.Data
{
    public class LotteryGameRepository : ILotteryGameRepository
    {
        private IConnectionFactory _connectionFactory;

        public LotteryGameRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IList<LotteryGame> GetAll()
        {
            List<LotteryGame> lotteryGames = new List<LotteryGame>();
            SqlConnection connection = _connectionFactory.CreateSqlConnection();
            string select = "SELECT * " +
                            "FROM LotteryGames";
            SqlCommand command = new SqlCommand(select, connection);
            SqlDataReader reader = null;

            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    LotteryGame lotteryGame = new LotteryGame()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = Convert.ToString(reader["Name"]),
                        NumberOfNumbersInADraw = Convert.ToInt32(reader["NumberOfNumbersInADraw"]),
                        MaximumNumber = Convert.ToInt32(reader["MaximumNumber"])
                    };
                    lotteryGames.Add(lotteryGame);
                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally{
                connection?.Close();
                reader?.Close();
            }
            return lotteryGames;
        }
    }
}