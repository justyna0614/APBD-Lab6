using System.Data.Common;
using CodeFirst.EFConfiguration;
using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Context;

public class AppDbContext : DbContext //Tworzymy dwa konstruktowy dla DbContext
{
    public DbSet<Doctor> Doctor { get; set; } //Dodajemy DbSety dla naszych modeli
    public DbSet<Patient> Patient { get; set; }
    public DbSet<Prescription> Prescription { get; set; }
    public DbSet<Medicament> Medicament { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicament { get; set; }

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) :
        base(options) //Tworzymy konstruktor z parametrem options -> bo będziemy przekazywać opcje konfiguracji bazy danych
    {
    }

    // Pozwala sklasyfikować wsyzstkie metadane związane z tabelami -> jest tego dużo więc warto rozdzielić to do klas
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DoctorEfConfiguration());
        modelBuilder.ApplyConfiguration(new PatientEfConfiguration());
        modelBuilder.ApplyConfiguration(new PrescriptionEfConfiguration());
        modelBuilder.ApplyConfiguration(new MedicamentEdConfiguration());
        modelBuilder.ApplyConfiguration(new PrescriptionMedicamentEfConfiguration());
    }
}