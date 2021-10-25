using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class AircashSimulatorContext : DbContext
    {
        public AircashSimulatorContext(DbContextOptions<AircashSimulatorContext> options) : base(options)
        {
        }

        public DbSet<SettingEntity> Settings { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }
        //public DbSet<PartnerEntity> Partners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SettingEntity>().ToTable("Settings");
            modelBuilder.Entity<TransactionEntity>().ToTable("Transactions");
        }
    }
}