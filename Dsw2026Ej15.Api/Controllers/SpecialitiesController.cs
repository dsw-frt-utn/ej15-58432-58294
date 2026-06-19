using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/specialities")]
public class SpecialitiesController : ControllerBase
{
    private readonly IPersistence _persistence;

    public SpecialitiesController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_persistence.GetSpecialities());
    }

    [HttpPost]
    public IActionResult Post([FromBody] SpecialityInput input)
    {
        if (string.IsNullOrWhiteSpace(input.Name))
        {
            throw new ValidationException("El nombre de la especialidad es obligatorio.");
        }

        var speciality = new Speciality
        {
            Name = input.Name,
            Description = input.Description
        };

        _persistence.AddSpeciality(speciality);
        return Created(string.Empty, speciality);
    }
}