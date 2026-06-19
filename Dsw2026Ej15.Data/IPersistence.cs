using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Data;

public interface IPersistence
{
    void AddSpeciality(Speciality speciality);
    void AddDoctor(Doctor doctor);
    IEnumerable<Speciality> GetSpecialities();
    IEnumerable<Doctor> GetDoctors();
}