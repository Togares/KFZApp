using System;
using CommonTypes;
using System.Data;
using Mk.DBConnector;
using System.Collections.Generic;

namespace DataAccess
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
                System.Diagnostics.Debug.WriteLine("Creating MySqlConnection...");
                _Instance = new MySqlConnection();
            }
            return _Instance;
        }

        #endregion

        public bool IsConnected { get; set; }

        private DBAdapter _SqlAdapter;

        public void Connect()
        {
            if (!IsConnected)
            {
                try
                {
                    _SqlAdapter = new DBAdapter(DatabaseType.MySql, Instance.Singleton, "localhost", 3306, "test", "defaultuser", "password", "dbconnector.log");
                    _SqlAdapter.Adapter.LogFile = true;
                    IsConnected = true;
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.Print("Connect failed\n");
                }
            }
            else
            {
                System.Diagnostics.Debug.Print("Already connected...\n");
            }
        }

        public DBAdapter GetAdapter()
        {
            if (IsConnected && _SqlAdapter != null)
            {
                return _SqlAdapter;
            }
            return null;
        }
    }
}
