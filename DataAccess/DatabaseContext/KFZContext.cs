using CommonTypes;
using System.Data.Entity;
using DataAccess.Connection;

namespace DataAccess.DatabaseContext
{
    public class KFZContext : ContextBase
    {
        public KFZContext(IConnection connection, bool ownsConnection)
            : base(connection, ownsConnection)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("kfz");

            modelBuilder.Entity<KFZ>().ToTable("kfz");
            modelBuilder.Entity<KFZ>().Property(x => x.ID).HasColumnName("id");
            modelBuilder.Entity<KFZ>().Property(x => x.IDTyp).HasColumnName("id_typ");
            modelBuilder.Entity<KFZ>().Property(x => x.Kennzeichen).HasColumnName("kennzeichen");
            modelBuilder.Entity<KFZ>().Property(x => x.FahrgestellNR).HasColumnName("fahrgestell");
            modelBuilder.Entity<KFZ>().Property(x => x.Leistung).HasColumnName("leistung");
            modelBuilder.Entity<KFZ>().HasRequired(x => x.Typ).WithMany().HasForeignKey(x => x.IDTyp);

            modelBuilder.Entity<KFZTyp>().ToTable("typ");
            modelBuilder.Entity<KFZTyp>().Property(x => x.ID).HasColumnName("id");
            modelBuilder.Entity<KFZTyp>().Property(x => x.Beschreibung).HasColumnName("beschreibung");

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<KFZ> Fahrzeuge { get; set; }
        public virtual DbSet<KFZTyp> Types { get; set; }
    }
}
