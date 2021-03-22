
namespace DataAccess.Connection
{
    public interface IConnection
    {
        void Connect(string connectionString = null);
        void Disconnect();
        bool IsConnected { get; set; }
    }
}
