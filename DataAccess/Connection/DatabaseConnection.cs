namespace DataAccess.Connection
{
    public class DatabaseConnection
    {
        #region Singleton

        private DatabaseConnection()
        {

        }

        private static DatabaseConnection _Instance;

        public static DatabaseConnection GetInstannce()
        {
            return _Instance = _Instance == null ? new DatabaseConnection() : _Instance;
        }

        #endregion

        public IConnection ActualConnection { get; set; }

        public enum DatabaseType
        {
            MySql,
            PostgreSql
        }

        public DatabaseType Database { get; set; } = DatabaseType.MySql;

        public string ConnectionString { get; set; }

        public void Connect()
        {
            // Disconnect because the Database type may have changed
            if (ActualConnection != null && ActualConnection.IsConnected)
                Disconnect();

            switch (Database)
            {
                case DatabaseType.MySql:
                    ActualConnection = new MySqlConnection();
                    break;
                case DatabaseType.PostgreSql:
                    ActualConnection = new PostgreSqlConnection();
                    break;
                default:
                    break;
            }
            ActualConnection.Connect(ConnectionString);
        }

        public void Disconnect()
        {
            ActualConnection.Disconnect();
        }
    }
}
