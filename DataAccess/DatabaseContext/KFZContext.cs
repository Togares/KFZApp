using CommonTypes;
using MySql.Data.Entity;
using System.Data.Entity;

namespace DataAccess.DatabaseContext
{
    public class KFZContext : DbContext
    {
        public KFZContext() : base()
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
