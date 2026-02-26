using Clinic.Application.DTOs;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Clinic.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using infraEntity = Clinic.Infrastructure.Persistence.Entities.Medicine;

namespace Clinic.Infrastructure.Repositories;

public class MedicineRepository : IMedicineRepository
{
    private readonly ClinicDbContext _context;

    public MedicineRepository(ClinicDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Medicine?> GetByGuidAsync(Guid medicineGuid)
    {
        var entity = await _context.Medicines
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MedicineGuid == medicineGuid && m.IsActive);

        if (entity == null) return null;

        return MapToDomain(entity);
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
        var entities = await _context.Medicines
            .AsNoTracking()
            .Where(m => m.IsActive)
            .ToListAsync();

        return entities.Select(MapToDomain).ToList();
    }

    public async Task<List<MedicineSearchDto>> SearchByNameAsync(string term)
    {
        if (string.IsNullOrWhiteSpace(term))
            return new List<MedicineSearchDto>();

        term = term.Trim();

        var list = await _context.Medicines
            .AsNoTracking()
            .Where(m => m.IsActive && EF.Functions.Like(m.Name, $"%{term}%"))
            .Select(m => new MedicineSearchDto { MedicineId = m.MedicineId, Name = m.Name })
            .ToListAsync();

        return list;
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

    private static Medicine MapToDomain(infraEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        return new Medicine(entity.MedicineGuid, entity.Name, entity.Description);
    }
}