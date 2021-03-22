using System.Data.Common;
using System.Data.Entity;

namespace DataAccess.DatabaseContext
{
    public abstract class ContextBase : DbContext
    {
        public ContextBase(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
            // Source http://robsneuron.blogspot.com/2013/11/entity-framework-upgrade-to-6.html

            // ROLA - This is a hack to ensure that Entity Framework SQL Provider is copied across to the output folder.
            // As it is installed in the GAC, Copy Local does not work. It is required for probing.
            // Fixed "Provider not loaded" error
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
