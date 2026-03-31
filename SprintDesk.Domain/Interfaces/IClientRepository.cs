using CaseFlow.Domain.Entity;

namespace CaseFlow.Domain.Interfaces;

public interface IClientRepository
{
    Task<List<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(Guid id);
    Task<List<Client>> GetByOrganizationIdAsync(Guid organizationId);
    Task<Client> CreateAsync(Client client);
    Task<Client?> UpdateAsync(Client client);
    Task<bool> DeleteAsync(Guid id);
}
