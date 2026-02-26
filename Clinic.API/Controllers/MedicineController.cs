using Clinic.Application.DTOs;
using Clinic.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicineController : ControllerBase
{
    private readonly IMedicineService medicineService;
    public MedicineController(IMedicineService medicineService)
    {
        this.medicineService = medicineService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMedicines()
    {
        var medicines = await medicineService.GetAllAsync();
        return Ok(medicines);
    }

    [HttpPost]
    public async Task<IActionResult> AddMedicine([FromBody] CreateMedicineRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var createdMedicine = await medicineService.AddAsync(request);
        return CreatedAtAction(nameof(GetAllMedicines), createdMedicine);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMedicine(Guid id, [FromBody] MedicineUpdateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Validate existence first
        var existing = await medicineService.GetByGuidAsync(id);
        if (existing == null)
            return NotFound();

        // Forward update request and id to service
        await medicineService.UpdateAsync(id, request);
        return Ok(request);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var ok = await medicineService.DeleteAsync(id);

        if (!ok) 
            return NotFound();

        return Ok("Medicine softly deleted successfully.");
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? term)
    {
        var results = await medicineService.SearchAsync(term);
        return Ok(results);
    }

}