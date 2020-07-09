using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext()
        {

        }

        public SalesContext(DbContextOptions<SalesContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity
                .Property(p => p.Name)
                .IsUnicode();

                entity
                .Property(p => p.Description)
                .HasDefaultValue("No description");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity
                .Property(c => c.Name)
                .IsUnicode();

                entity
                .Property(c => c.Email)
                .IsUnicode(false);

                entity
                .Property(c => c.CreditCardNumber)
                .IsUnicode(false);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity
                .Property(s => s.Name)
                .IsUnicode();
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity
                .Property(s => s.Date)
                .HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
