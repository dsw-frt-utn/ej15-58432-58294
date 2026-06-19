using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Data;

public class PersistenceInMemory : IPersistence
{
    private readonly List<Speciality> _specialities = new();
    private readonly List<Doctor> _doctors = new();

    public PersistenceInMemory()
    {
        var cardio = new Speciality { Name = "Cardiología", Description = "Enfermedades del corazón" };
        var pedia = new Speciality { Name = "Pediatría", Description = "Atención médica infantil" };
        var neuro = new Speciality { Name = "Neurología", Description = "Trastornos del sistema nervioso" };

        _specialities.Add(cardio);
        _specialities.Add(pedia);
        _specialities.Add(neuro);

        _doctors.Add(new Doctor { Name = "Dr. Lucas Tomás Ferreyra", LicenseNumber = "M58432", IsActive = true, Speciality = cardio });
        _doctors.Add(new Doctor { Name = "Dr. Ignacio Matías Ferreyra", LicenseNumber = "M58294", IsActive = true, Speciality = cardio });
        _doctors.Add(new Doctor { Name = "Dr. Francisco Vicente", LicenseNumber = "M90123", IsActive = true, Speciality = pedia });
        _doctors.Add(new Doctor { Name = "Dr. Iñaki Moyano", LicenseNumber = "M34567", IsActive = false, Speciality = pedia });
        _doctors.Add(new Doctor { Name = "Dr. Alberto Moyano", LicenseNumber = "M78901", IsActive = true, Speciality = neuro });
        _doctors.Add(new Doctor { Name = "Dr. Vicente Chibilisco", LicenseNumber = "M11223", IsActive = true, Speciality = neuro });
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