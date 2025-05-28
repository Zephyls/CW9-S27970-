namespace Hospital.Controllers;
using Hospital.DTOs;
using Hospital.Exceptions;
using Hospital.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase {
    private readonly IPrescriptionService _svc;
    public PrescriptionsController(IPrescriptionService svc) {
        _svc = svc;
    }

    // POST /api/Prescriptions
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] NewPrescriptionDto dto) {
        try {
            var presDto = await _svc.AddAsync(dto);
            return CreatedAtAction(nameof(GetPatient),
                new { idPatient = dto.IdPatient },
                presDto);
        }
        catch (BusinessException ex) {
            return BadRequest(new { error = ex.Message });
        }
    }

    // GET /api/Prescriptions/patient/{id}
    [HttpGet("patient/{idPatient}")]
    public async Task<IActionResult> GetPatient(int idPatient) {
        try {
            var result = await _svc.GetPatientAsync(idPatient);
            return Ok(result);
        }
        catch (NotFoundException) {
            return NotFound();
        }
    }
}