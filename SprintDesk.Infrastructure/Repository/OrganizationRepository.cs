
using CaseFlow.Domain.Entity;
using CaseFlow.Domain.Interfaces;
using CaseFlow.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.Infrastructure.Repository;

public class OrganizationRepository:IOrganizationRepository
{

    private readonly AppDbContext _context;

    public OrganizationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Organization>> GetAllOrganizationsAsync()
    {
        return await _context.Organizations.ToListAsync();
    }
}
