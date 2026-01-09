using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Domain.Entities;

namespace Clinic.Application.Interfaces.Repositories
{
    public interface IMedicineRepository
    {
        Task AddAsync(Medicine medicine);
        Task UpdateAsync(Medicine medicine);
        Task<List<Medicine>> GetAllAsync();
        Task<bool> DeleteAsync(Guid medicineId);
    }
}
