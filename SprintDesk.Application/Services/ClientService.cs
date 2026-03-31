using CaseFlow.Application.DTOs.Client.Requests;
using CaseFlow.Application.DTOs.Client.Responses;
using CaseFlow.Application.Interfaces;
using CaseFlow.Domain.Entity;
using CaseFlow.Domain.Interfaces;

namespace CaseFlow.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _repository;

    public ClientService(IClientRepository repository) => _repository = repository;

    public async Task<List<ClientResponse>> GetAllAsync()
        => (await _repository.GetAllAsync()).Select(MapToResponse).ToList();

    public async Task<ClientResponse?> GetByIdAsync(Guid id)
    {
        var client = await _repository.GetByIdAsync(id);
        return client is null ? null : MapToResponse(client);
    }

    public async Task<List<ClientResponse>> GetByOrganizationIdAsync(Guid organizationId)
        => (await _repository.GetByOrganizationIdAsync(organizationId)).Select(MapToResponse).ToList();

    public async Task<ClientResponse> CreateAsync(ClientRequest request)
    {
        var client = new Client
        {
            Id = Guid.NewGuid(),
            OrganizationId = request.OrganizationId,
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            Address = request.Address,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        return MapToResponse(await _repository.CreateAsync(client));
    }

    public async Task<ClientResponse?> UpdateAsync(Guid id, ClientRequest request)
    {
        var client = new Client
        {
            Id = id,
            OrganizationId = request.OrganizationId,
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            Address = request.Address,
            UpdatedAt = DateTime.UtcNow
        };
        var updated = await _repository.UpdateAsync(client);
        return updated is null ? null : MapToResponse(updated);
    }

    public async Task<bool> DeleteAsync(Guid id)
        => await _repository.DeleteAsync(id);

    private static ClientResponse MapToResponse(Client c) => new()
    {
        Id = c.Id,
        OrganizationId = c.OrganizationId,
        Name = c.Name,
        Email = c.Email,
        Phone = c.Phone,
        Address = c.Address,
        CreatedAt = c.CreatedAt,
        UpdatedAt = c.UpdatedAt
    };
}
