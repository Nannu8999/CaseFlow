using CaseFlow.Domain.Entity;

namespace CaseFlow.Domain.Interfaces;

public interface ICaseFileRepository
{
    Task<List<CaseFile>> GetByCaseIdAsync(Guid caseId);
    Task<CaseFile?> GetByIdAsync(Guid id);
    Task<CaseFile> CreateAsync(CaseFile caseFile);
    Task<bool> DeleteAsync(Guid id);
}
