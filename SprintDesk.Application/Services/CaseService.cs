using CaseFlow.Application.DTOs.Cases.Requests;
using CaseFlow.Application.DTOs.Cases.Responses;
using CaseFlow.Application.Interfaces;
using CaseFlow.Domain.Interfaces;

namespace CaseFlow.Application.Services;

public class CaseService : ICaseService
{
    private readonly ICaseRepository _repository;

    public CaseService(ICaseRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<List<CaseResponse>> GetAllAsync()
        => (await _repository.GetAllAsync()).Select(MapToResponse).ToList();

    public async Task<CaseResponse?> GetByIdAsync(Guid id)
    {
        var c = await _repository.GetByIdAsync(id);
        return c is null ? null : MapToResponse(c);
    }

    public async Task<List<CaseResponse>> GetByOrganizationIdAsync(Guid organizationId)
        => (await _repository.GetByOrganizationIdAsync(organizationId)).Select(MapToResponse).ToList();

    public async Task<List<CaseResponse>> GetByClientIdAsync(Guid clientId)
        => (await _repository.GetByClientIdAsync(clientId)).Select(MapToResponse).ToList();

    public async Task<List<CaseResponse>> GetByAssignedUserIdAsync(Guid userId)
        => (await _repository.GetByAssignedUserIdAsync(userId)).Select(MapToResponse).ToList();

    public async Task<CaseResponse> CreateAsync(CaseRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var caseEntity = new CaseFlow.Domain.Entity.Case
        {
            Id = Guid.NewGuid(),
            OrganizationId = request.OrganizationId,
            ClientId = request.ClientId,
            AssignedTo = request.AssignedTo,
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            Status = request.Status,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        return MapToResponse(await _repository.CreateAsync(caseEntity));
    }

    public async Task<CaseResponse?> UpdateAsync(Guid id, CaseRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var caseEntity = new CaseFlow.Domain.Entity.Case
        {
            Id = id,
            OrganizationId = request.OrganizationId,
            ClientId = request.ClientId,
            AssignedTo = request.AssignedTo,
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            Status = request.Status,
            UpdatedAt = DateTime.UtcNow
        };
        var updated = await _repository.UpdateAsync(caseEntity);
        return updated is null ? null : MapToResponse(updated);
    }

    public async Task<bool> DeleteAsync(Guid id)
        => await _repository.DeleteAsync(id);

    private static CaseResponse MapToResponse(CaseFlow.Domain.Entity.Case c) => new()
    {
        Id = c.Id,
        OrganizationId = c.OrganizationId,
        ClientId = c.ClientId,
        AssignedTo = c.AssignedTo,
        Title = c.Title,
        Description = c.Description,
        Priority = c.Priority,
        Status = c.Status,
        CreatedAt = c.CreatedAt,
        UpdatedAt = c.UpdatedAt
    };
}

