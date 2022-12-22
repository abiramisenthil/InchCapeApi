using Microsoft.EntityFrameworkCore;
using WebApiUsingEF.Model;

namespace WebApiUsingEF.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<CarFinance> CarFinances { get; set; }

        public DbSet<CarFinanceRange> CarFinanceRanges { get; set; }

        public virtual DbSet<Make> Makes { get; set; }

        public DbSet<VehicleType> VehicleTypes { get; set; }
               
        public DbSet<FinanceType> FinanceTypes { get; set; }

        public DbSet<FinanceRange> FinanceRanges { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CarFinance>(entity =>{
                entity.ToTable("CarFinance");

                entity.HasOne(d => d.Make)
                .WithMany(d => d.CarFinances)
                .HasForeignKey(d => d.MakeId)
                .HasConstraintName("FK_CarFinance_Make");

                entity.HasOne(d => d.VehicleType)
                .WithMany(d => d.CarFinances)
                .HasForeignKey(d => d.VehicleTypeId)
                .HasConstraintName("FK_CarFinance_VehicleType");

                entity.HasOne(d => d.FinanceType)
                .WithMany(d => d.CarFinances)
                .HasForeignKey(d => d.FinanceTypeId)
                .HasConstraintName("FK_CarFinance_FinanceType");

            });

            builder.Entity<CarFinanceRange>(entity => {
                entity.HasKey(d => new { d.CarFinanceId, d.FinanceRangeId });
                entity.ToTable("CarFinanceRange");

                entity.HasOne(d => d.CarFinance)
                .WithMany(d => d.CarFinanceRanges)
                .HasForeignKey(d => d.CarFinanceId)
                .HasConstraintName("FK_CarFinanceRange_CarFinance");

                entity.HasOne(d => d.FinanceRange)
               .WithMany(d => d.CarFinanceRanges)
               .HasForeignKey(d => d.FinanceRangeId)
               .HasConstraintName("FK_CarFinanceRange_FinanceRange");

            });

            builder.Entity<Make>(entity => {
                entity.ToTable("Make");
            });

            builder.Entity<VehicleType>(entity => {
                entity.ToTable("VehicleType");
            });

            builder.Entity<FinanceType>(entity => {
                entity.ToTable("FinanceType");
            });

            builder.Entity<FinanceRange>(entity => {
                entity.ToTable("FinanceRange");
            });

            base.OnModelCreating(builder);
        }

    }
}
