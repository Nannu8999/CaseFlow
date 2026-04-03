using CaseFlow.Application.DTOs.Organization.Requests;
using CaseFlow.Application.DTOs.Organization.Responses;
using CaseFlow.Application.Interfaces;
using CaseFlow.Domain.Entity;
using CaseFlow.Domain.Interfaces;

namespace CaseFlow.Application.Services;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository _organizationRepository;

    public OrganizationService(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
    }

    public async Task<List<OrganizationResponse>> GetAllOrganizationsAsync()
    {
        var orgs = await _organizationRepository.GetAllOrganizationsAsync();
        return orgs.Select(MapToResponse).ToList();
    }

    public async Task<OrganizationResponse?> GetOrganizationByIdAsync(Guid id)
    {
        var org = await _organizationRepository.GetByIdAsync(id);
        return org is null ? null : MapToResponse(org);
    }

    public async Task<OrganizationResponse> CreateOrganizationAsync(OrganizationRequest request)
    {
        var organization = new Organization
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _organizationRepository.CreateAsync(organization);
        return MapToResponse(created);
    }

    public async Task<OrganizationResponse?> UpdateOrganizationAsync(Guid id, OrganizationRequest request)
    {
        var organization = new Organization
        {
            Id = id,
            Name = request.Name,
            UpdatedAt = DateTime.UtcNow
        };

        var updated = await _organizationRepository.UpdateAsync(organization);
        return updated is null ? null : MapToResponse(updated);
    }

    public async Task<bool> DeleteOrganizationAsync(Guid id)
    {
        return await _organizationRepository.DeleteAsync(id);
    }

    private static OrganizationResponse MapToResponse(Organization org) => new()
    {
        Id = org.Id,
        Name = org.Name,
        CreatedAt = org.CreatedAt,
        UpdatedAt = org.UpdatedAt
    };
}
