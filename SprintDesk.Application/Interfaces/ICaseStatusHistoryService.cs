using CaseFlow.Application.DTOs.Cases.Requests;
using CaseFlow.Application.DTOs.Cases.Responses;

namespace CaseFlow.Application.Interfaces;

public interface ICaseStatusHistoryService
{
    Task<List<CaseStatusHistoryResponse>> GetByCaseIdAsync(Guid caseId);
    Task<CaseStatusHistoryResponse> CreateAsync(CaseStatusHistoryRequest request);
}
