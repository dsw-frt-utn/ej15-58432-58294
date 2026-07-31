using Microsoft.EntityFrameworkCore;
﻿using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Data;

public class PersistenceEf : IPersistence
{
    private readonly MedicalContext _context;

    public PersistenceEf(MedicalContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
        SeedData();
    }

    public IEnumerable<Doctor> GetDoctors()
    {
        return _context.Doctors.Include(d => d.Speciality).ToList();
    }

    public IEnumerable<Speciality> GetSpecialities()
    {
        return _context.Specialities.ToList();
    }

    public void AddDoctor(Doctor doctor)
    {
        var speciality = _context.Specialities.FirstOrDefault(s => s.Id == doctor.Speciality.Id);
        if (speciality != null)
        {
            doctor.Speciality = speciality;
        }
        _context.Doctors.Add(doctor);
        _context.SaveChanges();
    }

    public void AddSpeciality(Speciality speciality)
    {
        _context.Specialities.Add(speciality);
        _context.SaveChanges();
    }

    private void SeedData()
    {
        if (_context.Specialities.Any()) return;

        var cardiologia = new Speciality { Id = Guid.NewGuid(), Name = "Cardiología", Description = "Enfermedades del corazón" };
        var pediatria = new Speciality { Id = Guid.NewGuid(), Name = "Pediatría", Description = "Atención médica infantil" };
        var neurologia = new Speciality { Id = Guid.NewGuid(), Name = "Neurología", Description = "Trastornos del sistema nervioso" };

        _context.Specialities.AddRange(cardiologia, pediatria, neurologia);

        var medicosIniciales = new List<Doctor>
        {
            new Doctor { Id = Guid.NewGuid(), Name = "Dr. Lucas Ferreyra", LicenseNumber = "M58294", IsActive = true, Speciality = cardiologia },
            new Doctor { Id = Guid.NewGuid(), Name = "Dr. Alberto Moyano", LicenseNumber = "M78901", IsActive = true, Speciality = neurologia }
        };

        _context.Doctors.AddRange(medicosIniciales);
        _context.SaveChanges();
    }
}