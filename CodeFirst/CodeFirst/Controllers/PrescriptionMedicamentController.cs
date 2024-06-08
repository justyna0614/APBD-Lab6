using CodeFirst.Context;
using CodeFirst.Controllers.DTO;
using CodeFirst.Models;
using CodeFirst.Services;
using CodeFirst.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Controllers;

[Route("/api/doctors")]
[ApiController]
public class PrescriptionMedicamentController : ControllerBase
{
    private readonly PrescriptionMedicamentService _service;

    public PrescriptionMedicamentController(PrescriptionMedicamentService service)
    {
        _service = service;
    }

    [HttpPost("{id:int}")]
    public async Task<IActionResult> AddPrescriptionMedicamentAsync(int id, [FromBody] CreateNewPrescriptionDTO request)
    {
        try
        {
            var result = await _service.AddPrescriptionMedicamentAsync(id, request);
            if (result)
            {
                return Ok();
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        catch (TooManyMedicamentException e)
        {
            return BadRequest("Too many medicaments");
        }
        catch (WrongDueDateException e)
        {
            return BadRequest("Wrong due date");
        }
        catch (MedicamentNotFoundException e)
        {
            return NotFound("Medicament with given id does not exist");
        }
    }
}