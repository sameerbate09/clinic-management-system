using Clinic.Application.DTOs;
using Clinic.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers
{
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

        [HttpPut]
        public async Task<IActionResult> UpdateMedicine([FromBody] MedicineUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await  medicineService.UpdateAsync(request);
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

    }
}
