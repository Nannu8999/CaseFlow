using CaseFlow.Application.DTOs.Cases.Requests;
using CaseFlow.Application.DTOs.Cases.Responses;

namespace CaseFlow.Application.Interfaces;

public interface ICaseService
{
    Task<List<CaseResponse>> GetAllAsync();
    Task<CaseResponse?> GetByIdAsync(Guid id);
    Task<List<CaseResponse>> GetByOrganizationIdAsync(Guid organizationId);
    Task<List<CaseResponse>> GetByClientIdAsync(Guid clientId);
    Task<List<CaseResponse>> GetByAssignedUserIdAsync(Guid userId);
    Task<CaseResponse> CreateAsync(CaseRequest request);
    Task<CaseResponse?> UpdateAsync(Guid id, CaseRequest request);
    Task<bool> DeleteAsync(Guid id);
}
