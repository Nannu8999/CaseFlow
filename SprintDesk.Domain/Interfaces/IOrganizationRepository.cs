
using CaseFlow.Domain.Entity;

namespace CaseFlow.Domain.Interfaces;

public interface IOrganizationRepository
{
    Task<List<Organization>> GetAllOrganizationsAsync();
}
