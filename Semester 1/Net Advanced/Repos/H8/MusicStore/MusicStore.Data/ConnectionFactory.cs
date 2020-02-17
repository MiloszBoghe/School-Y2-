﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Data
{
    internal static class ConnectionFactory
    {
        public static SqlConnection CreateSqlConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MusicStoreConnectionString"].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}