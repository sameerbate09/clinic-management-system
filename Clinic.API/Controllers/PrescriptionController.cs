using Clinic.Application.DTOs;
using Clinic.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _service;

    public PrescriptionController(IPrescriptionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePrescriptionRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _service.CreateAsync(request);
        return Ok(id);
    }

    [HttpGet("{guid}")]
    public async Task<IActionResult> Get(Guid guid)
    {
        var result = await _service.GetAsync(guid);
        if (result == null) return NotFound();

        return Ok(result);
    }

    [HttpGet("by-visit/{visitGuid}")]
    public async Task<IActionResult> GetByVisit(Guid visitGuid)
    {
        var result = await _service.GetByVisitGuidAsync(visitGuid);
        if (result == null) return NotFound();

        return Ok(result);
    }

    [HttpPut("{guid}")]
    public async Task<IActionResult> UpdatePrescription(Guid guid, [FromBody] UpdatePrescriptionRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _service.UpdateAsync(guid, request);
        return NoContent();
    }

    [HttpPost("auto-finalize")]
    public async Task<IActionResult> AutoFinalize()
    {
        await _service.AutoFinalizeExpiredAsync();
        return Ok("Expired prescriptions finalized.");
    }
}