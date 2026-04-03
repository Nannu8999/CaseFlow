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
public class CaseStatusHistoryController : ControllerBase
{
    private readonly ICaseStatusHistoryService _historyService;

    public CaseStatusHistoryController(ICaseStatusHistoryService historyService)
        => _historyService = historyService;

    [HttpGet("case/{caseId:guid}")]
    public async Task<IActionResult> GetByCaseId(Guid caseId)
        => Ok(await _historyService.GetByCaseIdAsync(caseId));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CaseStatusHistoryRequest request)
    {
        var created = await _historyService.CreateAsync(request);
        return CreatedAtAction(nameof(GetByCaseId), new { caseId = created.CaseId }, created);
    }
}
