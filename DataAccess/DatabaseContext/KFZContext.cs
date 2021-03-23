using CommonTypes;
using System.Data.Entity;
using MySql.Data.EntityFramework;
using DataAccess.Connection;

namespace DataAccess.DatabaseContext
{
    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class KFZContext : ContextBase
    {
        public KFZContext(IConnection connection, bool ownsConnection)
            : base(connection, ownsConnection)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("kfz");
            modelBuilder.Entity<KFZ>().ToTable("kfz");
            modelBuilder.Entity<KFZ>().Property(x => x.ID).HasColumnName("id");
            modelBuilder.Entity<KFZ>().Property(x => x.Kennzeichen).HasColumnName("kennzeichen");
            modelBuilder.Entity<KFZ>().Property(x => x.FahrgestellNR).HasColumnName("fahrgestell");
            modelBuilder.Entity<KFZ>().Property(x => x.Leistung).HasColumnName("leistung");
            modelBuilder.Entity<KFZ>().Property(x => x.Typ).HasColumnName("typ");
        }

        public virtual DbSet<KFZ> Fahrzeuge { get; set; }
    }
}
