using Clinic.Application.DTOs;
using Clinic.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;

[Authorize]
[ApiController]
[Route("api/visits")]
public class VisitsController : ControllerBase
{
    private readonly IVisitService _visitService;

    public VisitsController(IVisitService visitService)
    {
        _visitService = visitService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVisitDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var visitId = await _visitService.AddAsync(dto);
        return Ok(new { VisitId = visitId });
    }

    [HttpGet("patient/{patientId:guid}")]
    public async Task<IActionResult> GetVisitHistory(Guid patientId)
    {
        return Ok(await _visitService.GetByPatientIdAsync(patientId));
    }

    [HttpGet("{visitId:guid}")]
    public async Task<IActionResult> GetById(Guid visitId)
    {
        var visit = await _visitService.GetByIdAsync(visitId);
        if (visit == null) return NotFound();

        return Ok(visit);
    }
}