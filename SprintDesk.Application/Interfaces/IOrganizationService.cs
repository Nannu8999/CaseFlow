using CaseFlow.Application.DTOs.Organization.Requests;
using CaseFlow.Application.DTOs.Organization.Responses;

namespace CaseFlow.Application.Interfaces;

public interface IOrganizationService
{
    Task<List<OrganizationResponse>> GetAllOrganizationsAsync();
    Task<OrganizationResponse?> GetOrganizationByIdAsync(Guid id);
    Task<OrganizationResponse> CreateOrganizationAsync(OrganizationRequest request);
    Task<OrganizationResponse?> UpdateOrganizationAsync(Guid id, OrganizationRequest request);
    Task<bool> DeleteOrganizationAsync(Guid id);
}
