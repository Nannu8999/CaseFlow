using CaseFlow.Application.DTOs.Cases.Requests;
using CaseFlow.Application.DTOs.Cases.Responses;
using CaseFlow.Application.Interfaces;
using CaseFlow.Domain.Entity;
using CaseFlow.Domain.Interfaces;

namespace CaseFlow.Application.Services;

public class CaseStatusHistoryService : ICaseStatusHistoryService
{
    private readonly ICaseStatusHistoryRepository _repository;

    public CaseStatusHistoryService(ICaseStatusHistoryRepository repository) => _repository = repository;

    public async Task<List<CaseStatusHistoryResponse>> GetByCaseIdAsync(Guid caseId)
        => (await _repository.GetByCaseIdAsync(caseId)).Select(MapToResponse).ToList();

    public async Task<CaseStatusHistoryResponse> CreateAsync(CaseStatusHistoryRequest request)
    {
        var history = new CaseStatusHistory
        {
            Id = Guid.NewGuid(),
            CaseId = request.CaseId,
            Status = request.Status,
            ChangedBy = request.ChangedBy,
            Remarks = request.Remarks,
            CreatedAt = DateTime.UtcNow
        };
        return MapToResponse(await _repository.CreateAsync(history));
    }

    private static CaseStatusHistoryResponse MapToResponse(CaseStatusHistory h) => new()
    {
        Id = h.Id,
        CaseId = h.CaseId,
        Status = h.Status,
        ChangedBy = h.ChangedBy,
        Remarks = h.Remarks,
        CreatedAt = h.CreatedAt
    };
}
