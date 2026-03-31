using CaseFlow.Domain.Entity;

namespace CaseFlow.Domain.Interfaces;

public interface ICaseStatusHistoryRepository
{
    Task<List<CaseStatusHistory>> GetByCaseIdAsync(Guid caseId);
    Task<CaseStatusHistory> CreateAsync(CaseStatusHistory history);
}
