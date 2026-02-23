using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Clinic.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using infraEntity = Clinic.Infrastructure.Persistence.Entities.Medicine;

namespace Clinic.Infrastructure.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly ClinicDbContext _context;

        public MedicineRepository(ClinicDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task AddAsync(Medicine medicine)
        {
            if (medicine is null)
                throw new ArgumentNullException(nameof(medicine), "Medicine cannot be null");

            // Map domain Medicine to persistence entity
            var entity = new infraEntity
            {
                Name = medicine.Name,
                Description = medicine.Description,
                // IsActive configured with defaults in the DbContext model;
            };
            await _context.Medicines.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid medicineId)
        {
            if (medicineId == null)
                throw new ArgumentNullException(nameof(medicineId));

            var entity = await _context.Medicines.FirstOrDefaultAsync(m => m.MedicineGuid == medicineId);
            if (entity == null)
                return false;

            // Soft-delete: mark inactive
            entity.IsActive = false;
            _context.Medicines.Update(entity);
            await _context.SaveChangesAsync();
            return true;


        }

        public async Task<List<Medicine>> GetAllAsync()
        {
            return await _context.Medicines
                .AsNoTracking()
                .Where(m => m.IsActive)
                .Select(m => new Medicine(m.MedicineGuid, m.Name, m.Description)
                {
                    MedicineGuid = m.MedicineGuid
                })
                .ToListAsync();
        }

        public async Task UpdateAsync(Medicine medicine)
        {
            if (medicine == null)
                throw new ArgumentNullException(nameof(medicine));

            var entity = await _context.Medicines
        .SingleOrDefaultAsync(m => m.MedicineGuid == medicine.MedicineGuid)
        ?? throw new InvalidOperationException("Medicine not found");

            // Update entity properties
            entity.Name = medicine.Name;
            entity.Description = medicine.Description;
            // Update other properties as needed

            _context.Medicines.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
