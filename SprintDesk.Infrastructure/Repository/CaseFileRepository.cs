using CaseFlow.Domain.Entity;
using CaseFlow.Domain.Interfaces;
using CaseFlow.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.Infrastructure.Repository;

public class CaseFileRepository : ICaseFileRepository
{
    private readonly AppDbContext _context;

    public CaseFileRepository(AppDbContext context) => _context = context;

    public async Task<List<CaseFile>> GetByCaseIdAsync(Guid caseId)
        => await _context.CaseFiles.AsNoTracking().Where(f => f.CaseId == caseId).ToListAsync();

    public async Task<CaseFile?> GetByIdAsync(Guid id)
        => await _context.CaseFiles.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);

    public async Task<CaseFile> CreateAsync(CaseFile caseFile)
    {
        _context.CaseFiles.Add(caseFile);
        await _context.SaveChangesAsync();
        return caseFile;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await _context.CaseFiles.FindAsync(id);
        if (existing is null) return false;

        _context.CaseFiles.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
