using Hospital.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Data;
public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> opts)
        : base(opts) { }

    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<Doctor> Doctors   { get; set; } = null!;
    public DbSet<Medicament> Medicaments { get; set; } = null!;
    public DbSet<Prescription> Prescriptions { get; set; } = null!;
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments
    { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder mb) {
        mb.Entity<PrescriptionMedicament>()
            .HasKey(pm => new { pm.PrescriptionId , pm.MedicamentId  });
        
        mb.Entity<PrescriptionMedicament>()
            .HasOne(pm => pm.Prescription)
            .WithMany(p => p.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.PrescriptionId );

        mb.Entity<PrescriptionMedicament>()
            .HasOne(pm => pm.Medicament)
            .WithMany(m => m.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.MedicamentId);
        
        mb.Entity<Prescription>()
            .Property(p => p.RowVersion)
            .IsRowVersion();
    }
}