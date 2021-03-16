using Mk.DBConnector;

namespace DataAccess
{
    public interface IConnection
    {
        void Connect();
        bool IsConnected { get; set; }
        DBAdapter GetAdapter();
    }
}
