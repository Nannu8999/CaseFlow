using CaseFlow.Application.DTOs.Cases.Requests;
using CaseFlow.Application.DTOs.Cases.Responses;

namespace CaseFlow.Application.Interfaces;

public interface ICaseFileService
{
    Task<List<CaseFileResponse>> GetByCaseIdAsync(Guid caseId);
    Task<CaseFileResponse?> GetByIdAsync(Guid id);
    Task<CaseFileResponse> CreateAsync(CaseFileRequest request);
    Task<bool> DeleteAsync(Guid id);
}
