using System.Data.Entity;
using DataAccess.Connection;

namespace DataAccess.DatabaseContext
{
    public abstract class ContextBase : DbContext
    {
        public ContextBase(IConnection existingConnection, bool contextOwnsConnection) : base(existingConnection.GetConnection(), contextOwnsConnection)
        {
            // Source http://robsneuron.blogspot.com/2013/11/entity-framework-upgrade-to-6.html

            // ROLA - This is a hack to ensure that Entity Framework SQL Provider is copied across to the output folder.
            // As it is installed in the GAC, Copy Local does not work. It is required for probing.
            // Fixed "Provider not loaded" error
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;            
        }
    }
}
