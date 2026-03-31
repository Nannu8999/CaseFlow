using CaseFlow.Domain.Entity;

namespace CaseFlow.Domain.Interfaces;

public interface ICaseRepository
{
    Task<List<Case>> GetAllAsync();
    Task<Case?> GetByIdAsync(Guid id);
    Task<List<Case>> GetByOrganizationIdAsync(Guid organizationId);
    Task<List<Case>> GetByClientIdAsync(Guid clientId);
    Task<List<Case>> GetByAssignedUserIdAsync(Guid userId);
    Task<Case> CreateAsync(Case caseEntity);
    Task<Case?> UpdateAsync(Case caseEntity);
    Task<bool> DeleteAsync(Guid id);
}
