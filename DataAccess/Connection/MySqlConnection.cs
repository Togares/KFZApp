using MySql.Data.MySqlClient;
using System;

namespace DataAccess.Connection
{
    public class MySqlConnection : IConnection
    {
        #region Singleton-Shit

        private MySqlConnection()
        {

        }

        private MySqlConnection(MySqlConnection other)
        {

        }

        private static MySqlConnection _Instance;

        public static MySqlConnection GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new MySqlConnection();
            }
            return _Instance;
        }

        #endregion

        // Don't get confused. This is the connection from MySqlConnector.
        // This class may need refactoring but I am lazy...
        private MySql.Data.MySqlClient.MySqlConnection _Connection;

        private string GetDefaultConnectionString()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Database = "test";
            builder.UserID = "root";
            builder.Password = "";
            builder.Port = 3306;
            builder.Server = "localhost";

            return builder.GetConnectionString(true);
        }

        public bool IsConnected { get; set; }

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
            if(IsConnected)
            {
                _Connection.Close();
                _Connection.Dispose();
            }
        }
    }
}
