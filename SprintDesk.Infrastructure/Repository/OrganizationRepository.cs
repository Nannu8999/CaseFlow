using CaseFlow.Domain.Entity;
using CaseFlow.Domain.Interfaces;
using CaseFlow.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.Infrastructure.Repository;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly AppDbContext _context;

    public OrganizationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Organization>> GetAllOrganizationsAsync()
    {
        return await _context.Organizations.AsNoTracking().ToListAsync();
    }

    public async Task<Organization?> GetByIdAsync(Guid id)
    {
        return await _context.Organizations.AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Organization> CreateAsync(Organization organization)
    {
        _context.Organizations.Add(organization);
        await _context.SaveChangesAsync();
        return organization;
    }

    public async Task<Organization?> UpdateAsync(Organization organization)
    {
        var existing = await _context.Organizations.FindAsync(organization.Id);
        if (existing is null) return null;

        existing.Name = organization.Name;
        existing.UpdatedAt = organization.UpdatedAt;
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await _context.Organizations.FindAsync(id);
        if (existing is null) return false;

        _context.Organizations.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
