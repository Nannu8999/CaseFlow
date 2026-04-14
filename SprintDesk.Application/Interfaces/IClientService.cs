using CaseFlow.Application.DTOs.Client.Requests;
using CaseFlow.Application.DTOs.Client.Responses;

namespace CaseFlow.Application.Interfaces;

public interface IClientService
{
    Task<List<ClientResponse>> GetAllAsync();
    Task<ClientResponse?> GetByIdAsync(Guid id);
    Task<List<ClientResponse>> GetByOrganizationIdAsync(Guid organizationId);
    Task<ClientResponse> CreateAsync(ClientRequest request, Guid organizationId);
    Task<ClientResponse?> UpdateAsync(Guid id, ClientRequest request, Guid organizationId);
    Task<bool> DeleteAsync(Guid id);
}
