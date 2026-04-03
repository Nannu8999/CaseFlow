using Asp.Versioning;
using CaseFlow.Application.DTOs.Cases.Requests;
using CaseFlow.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
public class CaseFileController : ControllerBase
{
    private readonly ICaseFileService _caseFileService;
    private readonly ILogger<CaseFileController> _logger;

    public CaseFileController(ICaseFileService caseFileService, ILogger<CaseFileController> logger)
    {
        _caseFileService = caseFileService;
        _logger = logger;
    }

    [HttpGet("case/{caseId:guid}")]
    public async Task<IActionResult> GetByCaseId(Guid caseId)
        => Ok(await _caseFileService.GetByCaseIdAsync(caseId));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var file = await _caseFileService.GetByIdAsync(id);
        if (file is null) { _logger.LogWarning("CaseFile {Id} not found.", id); return NotFound(); }
        return Ok(file);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CaseFileRequest request)
    {
        var created = await _caseFileService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _caseFileService.DeleteAsync(id);
        if (!deleted) { _logger.LogWarning("CaseFile {Id} not found for deletion.", id); return NotFound(); }
        return NoContent();
    }
}
