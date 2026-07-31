using Microsoft.EntityFrameworkCore;
using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Data;

public class MedicalContext : DbContext
{
    public MedicalContext(DbContextOptions<MedicalContext> options) : base(options) { }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Speciality> Specialities { get; set; }
}