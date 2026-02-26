using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Clinic.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using InfraEntity = Clinic.Infrastructure.Persistence.Entities.Therapy;

namespace Clinic.Infrastructure.Repositories;

public class TherapyRepository : ITherapyRepository
{
    private readonly ClinicDbContext _context;

    public TherapyRepository(ClinicDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task AddAsync(Therapy therapy)
    {
        if (therapy is null)
            throw new ArgumentNullException(nameof(therapy), "Therapy cannot be null");

        // Map domain Therapy to persistence entity
        var entity = new InfraEntity
        {
            TherapyName = therapy.Name,
            // IsActive configured with defaults in the DbContext model;
        };
        await _context.Therapies.AddAsync(entity);
        await _context.SaveChangesAsync();

        // Set generated id back on domain object so caller sees the new id
        therapy.Id = entity.TherapyId;
    }

    public async Task<List<Therapy>> GetAllAsync()
    {
        var list = await _context.Therapies
            .AsNoTracking()
            .Where(t => t.IsActive)
            .ToListAsync();

        return list.Select(MapToDomain).ToList();
    }

    public async Task UpdateAsync(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));

        var entity = await _context.Therapies
            .FirstOrDefaultAsync(t => t.TherapyId == id);

        if (entity == null)
            throw new InvalidOperationException("Therapy not found");

        entity.TherapyName = name;

        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int therapyId)
    {
        var entity = await _context.Therapies.FirstOrDefaultAsync(t => t.TherapyId == therapyId);
        if (entity == null)
            return false;

        // Soft-delete: mark inactive
        entity.IsActive = false;
        _context.Therapies.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ReactivateAsync(int therapyId)
    {
        var entity = await _context.Therapies.FirstOrDefaultAsync(t => t.TherapyId == therapyId);
        if (entity == null)
            return false;

        if (entity.IsActive)
            return true; // already active

        entity.IsActive = true;
        _context.Therapies.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    private static Therapy MapToDomain(InfraEntity entity)
    {
        // Map persistence entity to domain entity. Handle possible nulls defensively.
        var name = entity.TherapyName ?? string.Empty;
        return new Therapy(entity.TherapyId, name);
    }
}