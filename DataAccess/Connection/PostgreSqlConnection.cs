using Npgsql;
using System;
using System.Data.Common;
using System.Data.Entity;

namespace DataAccess.Connection
{
    public class PostgreSqlConnection : IConnection
    {
        public PostgreSqlConnection()
        {
            
        }

        private class NpgsqlConfiguration : DbConfiguration
        {
            public NpgsqlConfiguration() : base()
            {
                SetProviderFactory("Npgsql", NpgsqlFactory.Instance);
            }
        }

        private string GetDefaultConnectionString()
        {
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder();
            builder.Host = "localhost";
            builder.Database = "test";
            builder.Port = 5432;
            builder.UserName = "test";
            builder.Password = "test";
            builder.SslMode = SslMode.Disable;
            builder.Timeout = 15;
            builder.CommandTimeout = 15;

            return builder.ConnectionString;
        }

        private NpgsqlConnection _Connection;

        public bool IsConnected { get; set; }
        public DbConfiguration Configuration { get; set; } = new NpgsqlConfiguration();

        public void Connect(string connectionString = null)
        {
            if (!IsConnected)
            {
                try
                {
                    connectionString = connectionString == null ? GetDefaultConnectionString() : connectionString;
                    _Connection = new NpgsqlConnection(connectionString);
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
