using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {

        }

        public HospitalContext(DbContextOptions<HospitalContext> options)
            : base(options)
        {

        }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<PatientMedicament> PatientMedicaments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity
                .HasKey(d => d.DoctorId);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity
                .HasKey(p => p.PatientId);

                entity
                .Property(p => p.FirstName)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

                entity
                .Property(p => p.LastName)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

                entity
                .Property(p => p.Address)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(250);

                entity
                .Property(p => p.Email)
                .IsRequired(true)
                .IsUnicode(false)
                .HasMaxLength(80);
            });

            modelBuilder.Entity<Visitation>(entity =>
            {
                entity
                .HasKey(v => v.VisitationId);

                entity
                .Property(v => v.Comments)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(250);
            });

            modelBuilder.Entity<Diagnose>(entity =>
            {
                entity
                .HasKey(d => d.DiagnoseId);

                entity
                .Property(d => d.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

                entity
                .Property(d => d.Comments)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(250);
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity
               .HasKey(m => m.MedicamentId);

                entity
                .Property(m => m.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);
            });

            modelBuilder.Entity<PatientMedicament>(entity =>
            {
                entity.HasKey(pm => new { pm.PatientId, pm.MedicamentId });
            });
        }
    }
}
