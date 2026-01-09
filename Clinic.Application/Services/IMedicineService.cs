using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.DTOs;
using Clinic.Domain.Entities;

namespace Clinic.Application.Services
{
    public interface IMedicineService
    {
        Task<MedicineResponse> AddAsync(CreateMedicineRequest request);
        Task UpdateAsync(MedicineUpdateRequest request);
        Task<List<MedicineResponse>> GetAllAsync();
        Task<bool> DeleteAsync(Guid medicineId);
    }
}
