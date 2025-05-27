using System.Runtime.InteropServices.JavaScript;
using APDB_last_s27062.Models;
using Microsoft.EntityFrameworkCore;

namespace APDB_last_s27062.Data;

public class AppDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }

    private static DateTime _date1 = new DateTime(2023, 1, 29);
    private static DateTime _date2 = new DateTime(2025, 10, 13);
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var doctor = new Doctor
        {
            IdDoctor = 1,
            FirstName = "Anna",
            LastName = "Morris",
            Email = "anna.morris@gmail.com",
        };

        var patient = new Patient
        {
            IdPatient = 1,
            FirstName = "Mark",
            LastName = "Hauret",
            BirthDate = _date1
        };

        var medicament = new Medicament
        {
            IdMedicament = 1,
            Name = "Apap",
            Description = "lorem ipsum dolor sit amet",
            Type = "headache"
        };
        
        var prescription = new Prescription
        {
            IdPrescription = 1,
            Date = _date1,
            DueDate = _date2,
            IdPatient = 1,
            IdDoctor = 1
        };

        var prescriptionMedicament = new PrescriptionMedicament
        {
            IdMedicament = 1,
            IdPrescription = 1,
            Dose = null,
            Details = "lorem ipsum dolor sit amet"
        };
        
        modelBuilder.Entity<Doctor>().HasData(doctor);
        modelBuilder.Entity<Patient>().HasData(patient);
        modelBuilder.Entity<Medicament>().HasData(medicament);
        modelBuilder.Entity<Prescription>().HasData(prescription);
        modelBuilder.Entity<PrescriptionMedicament>().HasData(prescriptionMedicament);
    }
}