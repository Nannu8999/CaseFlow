using CaseFlow.Domain.Entity;
using CaseFlow.Domain.Interfaces;
using CaseFlow.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.Infrastructure.Repository;

public class CaseRepository : ICaseRepository
{
    private readonly AppDbContext _context;

    public CaseRepository(AppDbContext context) => _context = context;

    public async Task<List<Case>> GetAllAsync()
        => await _context.Cases.AsNoTracking().ToListAsync();

    public async Task<Case?> GetByIdAsync(Guid id)
        => await _context.Cases.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

    public async Task<List<Case>> GetByOrganizationIdAsync(Guid organizationId)
        => await _context.Cases.AsNoTracking().Where(c => c.OrganizationId == organizationId).ToListAsync();

    public async Task<List<Case>> GetByClientIdAsync(Guid clientId)
        => await _context.Cases.AsNoTracking().Where(c => c.ClientId == clientId).ToListAsync();

    public async Task<List<Case>> GetByAssignedUserIdAsync(Guid userId)
        => await _context.Cases.AsNoTracking().Where(c => c.AssignedTo == userId).ToListAsync();

    public async Task<Case> CreateAsync(Case caseEntity)
    {
        _context.Cases.Add(caseEntity);
        await _context.SaveChangesAsync();
        return caseEntity;
    }

    public async Task<Case?> UpdateAsync(Case caseEntity)
    {
        var existing = await _context.Cases.FindAsync(caseEntity.Id);
        if (existing is null) return null;

        existing.Title = caseEntity.Title;
        existing.Description = caseEntity.Description;
        existing.Priority = caseEntity.Priority;
        existing.Status = caseEntity.Status;
        existing.AssignedTo = caseEntity.AssignedTo;
        existing.ClientId = caseEntity.ClientId;
        existing.OrganizationId = caseEntity.OrganizationId;
        existing.UpdatedAt = caseEntity.UpdatedAt;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await _context.Cases.FindAsync(id);
        if (existing is null) return false;

        _context.Cases.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
