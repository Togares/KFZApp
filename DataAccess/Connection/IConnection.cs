
using System.Data.Common;
using System.Data.Entity;

namespace DataAccess.Connection
{
    public interface IConnection
    {
        void Connect(string connectionString = null);
        void Disconnect();
        bool IsConnected { get; set; }
        DbConnection GetConnection();
        DbConfiguration Configuration { get; set; }
    }
}
