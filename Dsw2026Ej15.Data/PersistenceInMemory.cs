using System.Text.Json;
using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Data;

public class PersistenceInMemory : IPersistence
{
    private readonly List<Speciality> _specialities = new();
    private readonly List<Doctor> _doctors = new();

    public PersistenceInMemory()
    {
        LoadSpecialities();

        var cardio = _specialities.FirstOrDefault(s => s.Name == "Cardiología");
        var pedia = _specialities.FirstOrDefault(s => s.Name == "Pediatría");
        var neuro = _specialities.FirstOrDefault(s => s.Name == "Neurología");

        if (cardio != null && pedia != null && neuro != null)
        {
            _doctors.Add(new Doctor { Name = "Dr. Lucas Tomás Ferreyra", LicenseNumber = "M58432", IsActive = true, Speciality = cardio });
            _doctors.Add(new Doctor { Name = "Dr. Ignacio Matías Ferreyra", LicenseNumber = "M58294", IsActive = true, Speciality = cardio });
            _doctors.Add(new Doctor { Name = "Dr. Francisco Vicente", LicenseNumber = "M90123", IsActive = true, Speciality = pedia });
            _doctors.Add(new Doctor { Name = "Dr. Iñaki Moyano", LicenseNumber = "M34567", IsActive = false, Speciality = pedia });
            _doctors.Add(new Doctor { Name = "Dr. Alberto Moyano", LicenseNumber = "M78901", IsActive = true, Speciality = neuro });
            _doctors.Add(new Doctor { Name = "Dr. Vicente Chibilisco", LicenseNumber = "M11223", IsActive = true, Speciality = neuro });
        }
    }

    private void LoadSpecialities()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "specialities.json");

        if (!File.Exists(path))
        {
            return;
        }

        var json = File.ReadAllText(path);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var items = JsonSerializer.Deserialize<List<SpecialityJsonDto>>(json, options);

        if (items == null)
        {
            return;
        }

        foreach (var item in items)
        {
            _specialities.Add(new Speciality
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description
            });
        }
    }

    private class SpecialityJsonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public void AddSpeciality(Speciality speciality)
    {
        _specialities.Add(speciality);
    }

    public void AddDoctor(Doctor doctor)
    {
        _doctors.Add(doctor);
    }

    public IEnumerable<Speciality> GetSpecialities()
    {
        return _specialities;
    }

    public IEnumerable<Doctor> GetDoctors()
    {
        return _doctors;
    }
}
