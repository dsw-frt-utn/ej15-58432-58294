using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain;
using System.Linq;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    [HttpPost]
    public IActionResult Post([FromBody] DoctorInput input)
    {
        if (string.IsNullOrWhiteSpace(input.Name))
        {
            throw new ValidationException("El nombre del médico es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(input.LicenseNumber))
        {
            throw new ValidationException("La matrícula es obligatoria.");
        }

        var matchMatricula = _persistence.GetDoctors()
            .Any(d => d.LicenseNumber.Equals(input.LicenseNumber, StringComparison.OrdinalIgnoreCase));

        if (matchMatricula)
        {
            throw new ValidationException("La matrícula especificada ya pertenece a otro médico.");
        }

        var speciality = _persistence.GetSpecialities()
            .FirstOrDefault(s => s.Name.Equals(input.SpecialityName, StringComparison.OrdinalIgnoreCase));

        if (speciality == null)
        {
            throw new ValidationException("La especialidad especificada no existe.");
        }

        var doctor = new Doctor
        {
            Name = input.Name,
            LicenseNumber = input.LicenseNumber,
            IsActive = true,
            Speciality = speciality
        };

        _persistence.AddDoctor(doctor);
        return Created(string.Empty, doctor);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var activeDoctors = _persistence.GetDoctors().Where(d => d.IsActive);
        return Ok(activeDoctors);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var doctor = _persistence.GetDoctors().FirstOrDefault(d => d.Id == id && d.IsActive);

        if (doctor == null)
        {
            return NotFound();
        }

        var result = new
        {
            doctor.Name,
            doctor.LicenseNumber,
            SpecialityName = doctor.Speciality.Name
        };

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var doctor = _persistence.GetDoctors().FirstOrDefault(d => d.Id == id && d.IsActive);

        if (doctor == null)
        {
            return NotFound();
        }

        doctor.IsActive = false;
        return NoContent();
    }
}