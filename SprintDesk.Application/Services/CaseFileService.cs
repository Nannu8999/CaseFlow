using CaseFlow.Application.DTOs.Cases.Requests;
using CaseFlow.Application.DTOs.Cases.Responses;
using CaseFlow.Application.Interfaces;
using CaseFlow.Domain.Entity;
using CaseFlow.Domain.Interfaces;

namespace CaseFlow.Application.Services;

public class CaseFileService : ICaseFileService
{
    private readonly ICaseFileRepository _repository;

    public CaseFileService(ICaseFileRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<List<CaseFileResponse>> GetByCaseIdAsync(Guid caseId)
        => (await _repository.GetByCaseIdAsync(caseId)).Select(MapToResponse).ToList();

    public async Task<CaseFileResponse?> GetByIdAsync(Guid id)
    {
        var file = await _repository.GetByIdAsync(id);
        return file is null ? null : MapToResponse(file);
    }

    public async Task<CaseFileResponse> CreateAsync(CaseFileRequest request)
    {
        var caseFile = new CaseFile
        {
            Id = Guid.NewGuid(),
            CaseId = request.CaseId,
            FileName = request.FileName,
            FilePath = request.FilePath,
            FileSize = request.FileSize,
            ContentType = request.ContentType,
            UploadedBy = request.UploadedBy,
            CreatedAt = DateTime.UtcNow
        };
        return MapToResponse(await _repository.CreateAsync(caseFile));
    }

    public async Task<bool> DeleteAsync(Guid id)
        => await _repository.DeleteAsync(id);

    private static CaseFileResponse MapToResponse(CaseFile f) => new()
    {
        Id = f.Id,
        CaseId = f.CaseId,
        FileName = f.FileName,
        FilePath = f.FilePath,
        FileSize = f.FileSize,
        ContentType = f.ContentType,
        UploadedBy = f.UploadedBy,
        CreatedAt = f.CreatedAt
    };
}
