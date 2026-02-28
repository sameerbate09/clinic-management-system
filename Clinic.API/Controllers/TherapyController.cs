using Clinic.Application.DTOs;
using Clinic.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TherapyController : ControllerBase
{
    private readonly ITherapyService _service;

    public TherapyController(ITherapyService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _service.GetAllTherapiesAsync();
        return Ok(list);
    }

    [HttpPost]
    public async Task<IActionResult> AddTherapy([FromBody] CreateTherapyRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var created = await _service.AddTherapyAsync(request);
        return CreatedAtAction(nameof(GetAll), created);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTherapy([FromQuery] int id, [FromBody] UpdateTherapyRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (request == null)
            return BadRequest("Request body is required");

        await _service.UpdateTherapyAsync(id, request.Name);
        return Ok(new { Id = id, Name = request.Name });
    }

    [HttpDelete("{therapyId}")]
    public async Task<IActionResult> DeleteTherapy(int therapyId)
    {
        await _service.DeleteTherapyAsync(therapyId);
        return Ok("Therapy is softly deleted successfully");
    }

    [HttpPost("{therapyId}/reactivate")]
    public async Task<IActionResult> ReactivateTherapy(int therapyId)
    {
        await _service.ReactivateTherapyAsync(therapyId);
        return Ok("Therapy reactivated successfully");
    }
}