using CaseFlow.Domain.Entity;
using CaseFlow.Domain.Interfaces;
using CaseFlow.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.Infrastructure.Repository;

public class CaseStatusHistoryRepository : ICaseStatusHistoryRepository
{
    private readonly AppDbContext _context;

    public CaseStatusHistoryRepository(AppDbContext context) => _context = context;

    public async Task<List<CaseStatusHistory>> GetByCaseIdAsync(Guid caseId)
        => await _context.CaseStatusHistories.AsNoTracking().Where(h => h.CaseId == caseId).ToListAsync();

    public async Task<CaseStatusHistory> CreateAsync(CaseStatusHistory history)
    {
        _context.CaseStatusHistories.Add(history);
        await _context.SaveChangesAsync();
        return history;
    }
}
