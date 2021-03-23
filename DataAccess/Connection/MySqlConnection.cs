﻿using System;
using System.Data.Common;
using System.Data.Entity;
using MySql.Data.EntityFramework;
using MySql.Data.MySqlClient;

namespace DataAccess.Connection
{
    public class MySqlConnection : IConnection
    {
        // Yes. This is a MySqlConnection within a MySqlConnection. MySqlConnectionCeption.
        private MySql.Data.MySqlClient.MySqlConnection _Connection;

        private string GetDefaultConnectionString()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.Database = "test";
            builder.UserID = "root";
            builder.Password = "";
            builder.Port = 3306;
            builder.SslMode = MySqlSslMode.None;

            return builder.ConnectionString;
        }

        public bool IsConnected { get; set; }
        public DbConfiguration Configuration { get; set; } = new MySqlEFConfiguration();

        public void Connect(string connectionString = null)
        {
            if (!IsConnected)
            {
                try
                {
                    connectionString = connectionString == null ? GetDefaultConnectionString() : connectionString;
                    _Connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                    _Connection.Open();
                    IsConnected = true;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                _Connection?.Close();
                _Connection?.Dispose();
                IsConnected = false;
            }
        }

        public DbConnection GetConnection()
        {
            return _Connection;
        }
    }
}
