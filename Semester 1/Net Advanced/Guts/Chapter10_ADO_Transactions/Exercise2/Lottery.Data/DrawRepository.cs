using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using Lottery.Data.Interfaces;
using Lottery.Domain;

namespace Lottery.Data
{
    public class DrawRepository : IDrawRepository
    {
        private IConnectionFactory _connectionFactory;
        public DrawRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IList<Draw> Find(int lotteryGameId, DateTime? fromDate, DateTime? untilDate)
        {
            SqlDataReader reader = null;
            ObservableCollection<Draw> drawList = new ObservableCollection<Draw>();
            SqlConnection connection = _connectionFactory.CreateSqlConnection();
            string selectStatement =
                "SELECT d.Id, d.LotteryGameId, d.Date, dn.DrawId, dn.Number, ISNULL(dn.Position,0) as Position " +
                "FROM Draws d " +
                "INNER JOIN DrawNumbers dn " +
                "ON d.Id = dn.DrawId " +
                "WHERE d.LotteryGameId = @lotteryGameId";

            SqlCommand selectCommand = new SqlCommand();
            selectCommand.Connection = connection;
            selectCommand.Parameters.AddWithValue("@lotteryGameId", lotteryGameId);
            if (fromDate != null)
            {
                selectStatement += " and d.Date >= @fromDate";
                selectCommand.Parameters.AddWithValue("@fromDate", fromDate);
            }
            if (untilDate != null)
            {
                selectStatement += " and d.Date <= @untilDate";
                selectCommand.Parameters.AddWithValue("@untilDate", untilDate);
            }
            selectCommand.CommandText = selectStatement;

            try
            {
                //variables
                ICollection<DrawNumber> drawNumbers = new Collection<DrawNumber>();
                Draw draw = null;
                DateTime date = DateTime.Now;
                int newId = 0;

                //start
                connection.Open();
                reader = selectCommand.ExecuteReader();
                if (!reader.Read())
                {
                    return drawList;
                }
                int id = Convert.ToInt32(reader["Id"]);
                newId = id;

                //First DrawNumber
                DrawNumber drawNumber = new DrawNumber()
                {
                    DrawId = id,
                    Number = Convert.ToInt32(reader["Number"]),
                    Position = Convert.ToInt32(reader["Position"])
                };
                drawNumbers.Add(drawNumber);
                while (reader.Read())
                {
                    newId = Convert.ToInt32(reader["Id"]);
                    date = Convert.ToDateTime(reader["Date"]);
                    if (id != newId)
                    {
                        draw = new Draw()
                        {
                            Id = id,
                            LotteryGameId = lotteryGameId,
                            Date = date,
                            DrawNumbers = drawNumbers
                        };
                        drawList.Add(draw);
                        drawNumbers = new Collection<DrawNumber>();
                        id = newId;
                    }
                    else
                    {
                        drawNumber = new DrawNumber
                        {
                            DrawId = newId,
                            Number = Convert.ToInt32(reader["Number"]),
                            Position = Convert.ToInt32(reader["Position"])
                        };
                        drawNumbers.Add(drawNumber);
                    }

                }
                draw = new Draw()
                {
                    Id = id,
                    LotteryGameId = lotteryGameId,
                    Date = date,
                    DrawNumbers = drawNumbers
                };
                drawList.Add(draw);
            }
            finally
            {
                reader?.Close();
                connection?.Close();
            }
            return drawList;
        }

        public void Add(int lotteryGameId, IList<int> numbers)
        {

            //NumbersCheck, lotterygameId Check
            if (numbers == null || numbers.Count == 0 || lotteryGameId <= 0)
            {
                throw new ArgumentException();
            }
            //connection, transaction
            SqlConnection connection = _connectionFactory.CreateSqlConnection();
            SqlTransaction drawTransaction = null;

            //Queries
            string idQuery = "Select MAX(Id)+1 " +
                                 "from Draws";

            string drawQuery = "SET IDENTITY_INSERT Draws ON " +
                               "Insert into Draws (Id,LotteryGameId, Date) values(@Id, @LotteryGameId, @Date) " +
                               "SET IDENTITY_INSERT Draws OFF";

            string drawNumberQuery = "SET IDENTITY_INSERT Draws ON " +
                                     "Insert into DrawNumbers (DrawId,Number,Position) values(@DrawId, @Number, @Position) " +
                                     "SET IDENTITY_INSERT Draws OFF";
            try
            {
                //start
                connection.Open();
                drawTransaction = connection.BeginTransaction();
                SqlCommand findDrawId = new SqlCommand(idQuery, connection, drawTransaction);
                SqlCommand insertDraw = new SqlCommand(drawQuery, connection, drawTransaction);
                SqlCommand insertDrawNumber = new SqlCommand();

                //Properties
                insertDrawNumber.Connection = connection;
                insertDrawNumber.Transaction = drawTransaction;
                insertDrawNumber.CommandText = drawNumberQuery;

                // new Id start
                int id = (int)findDrawId.ExecuteScalar();

                //Parameters & Inserts
                insertDraw.Parameters.AddWithValue("@Id", id);
                insertDraw.Parameters.AddWithValue("@LotteryGameId", lotteryGameId);
                insertDraw.Parameters.AddWithValue("@Date", DateTime.Now);

                //Execute & Commit
                insertDraw.ExecuteNonQuery();
                int position = 1;
                foreach (int num in numbers)
                {
                    //Parameters & Inserts
                    insertDrawNumber.Parameters.AddWithValue("@Number", num);
                    insertDrawNumber.Parameters.AddWithValue("@DrawId", id);
                    insertDrawNumber.Parameters.AddWithValue("@Position", position);
                    insertDrawNumber.ExecuteNonQuery();
                    insertDrawNumber.Parameters.Clear();
                    position++;
                }
                drawTransaction.Commit();
            }
            catch (Exception)
            {
                drawTransaction.Rollback();
            }
            finally
            {
                connection?.Close();
            }

        }
    }
}