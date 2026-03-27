using CaseFlow.Domain.Entity;

namespace CaseFlow.Domain.Interfaces;

public interface IOrganizationRepository
{
    Task<List<Organization>> GetAllOrganizationsAsync();
    Task<Organization?> GetByIdAsync(Guid id);
    Task<Organization> CreateAsync(Organization organization);
    Task<Organization?> UpdateAsync(Organization organization);
    Task<bool> DeleteAsync(Guid id);
}
