using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.DTOs;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;

namespace Clinic.Application.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository;
        public MedicineService(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }
       
        public async Task<MedicineResponse> AddAsync(CreateMedicineRequest request)
        {
            var domain = new Medicine(request.Name, request.Description);
            await _medicineRepository.AddAsync(domain);

            return new MedicineResponse
            {
                MedicineGuid = domain.MedicineGuid,
                Name = domain.Name,
                Description = domain.Description
            };
        }

        public Task<bool> DeleteAsync(Guid medicineId)
        {
            return _medicineRepository.DeleteAsync(medicineId);
        }

        public async Task<List<MedicineResponse>> GetAllAsync()
        {
            var list = await _medicineRepository.GetAllAsync();
            return list.Select(m => new MedicineResponse { MedicineGuid = m.MedicineGuid, Name = m.Name, Description = m.Description }).ToList();
        }

        public async Task UpdateAsync(MedicineUpdateRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var medicine = new Medicine(request.MedicineGuid, request.Name, request.Description);

           await  _medicineRepository.UpdateAsync(medicine);
        }

    }
}
