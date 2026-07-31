
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Speciality> Specialities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var specialities = LoadSpecialitiesFromJson();
        modelBuilder.Entity<Speciality>().HasData(specialities);
    }

    private List<Speciality> LoadSpecialitiesFromJson()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "specialities.json");
        var json = File.ReadAllText(path);

        var raw = JsonSerializer.Deserialize<List<SpecialityJson>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

        return raw.Select(s => new Speciality
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description
        }).ToList();
    }

    private class SpecialityJson
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

