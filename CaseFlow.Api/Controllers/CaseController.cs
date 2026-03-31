using CaseFlow.Application.DTOs.Cases.Requests;
using CaseFlow.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CaseController : ControllerBase
{
    private readonly ICaseService _caseService;
    private readonly ILogger<CaseController> _logger;

    public CaseController(ICaseService caseService, ILogger<CaseController> logger)
    {
        _caseService = caseService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _caseService.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var c = await _caseService.GetByIdAsync(id);
        if (c is null) { _logger.LogWarning("Case {Id} not found.", id); return NotFound(); }
        return Ok(c);
    }

    [HttpGet("organization/{organizationId:guid}")]
    public async Task<IActionResult> GetByOrganization(Guid organizationId)
        => Ok(await _caseService.GetByOrganizationIdAsync(organizationId));

    [HttpGet("client/{clientId:guid}")]
    public async Task<IActionResult> GetByClient(Guid clientId)
        => Ok(await _caseService.GetByClientIdAsync(clientId));

    [HttpGet("assigned/{userId:guid}")]
    public async Task<IActionResult> GetByAssignedUser(Guid userId)
        => Ok(await _caseService.GetByAssignedUserIdAsync(userId));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CaseRequest request)
    {
        var created = await _caseService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CaseRequest request)
    {
        var updated = await _caseService.UpdateAsync(id, request);
        if (updated is null) { _logger.LogWarning("Case {Id} not found for update.", id); return NotFound(); }
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _caseService.DeleteAsync(id);
        if (!deleted) { _logger.LogWarning("Case {Id} not found for deletion.", id); return NotFound(); }
        return NoContent();
    }
}
