using APDB_last_s27062.Models;
using Microsoft.EntityFrameworkCore;

namespace APDB_last_s27062.Data;

public class AppDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}