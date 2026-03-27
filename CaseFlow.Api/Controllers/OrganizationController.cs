using CaseFlow.Application.DTOs.Organization.Requests;
using CaseFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationService _organizationService;
    private readonly ILogger<OrganizationController> _logger;

    public OrganizationController(IOrganizationService organizationService, ILogger<OrganizationController> logger)
    {
        _organizationService = organizationService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrganizations()
    {
        var orgs = await _organizationService.GetAllOrganizationsAsync();
        return Ok(orgs);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrganizationById(Guid id)
    {
        var org = await _organizationService.GetOrganizationByIdAsync(id);
        if (org is null)
        {
            _logger.LogWarning("Organization with id {Id} not found.", id);
            return NotFound();
        }
        return Ok(org);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] OrganizationRequest request)
    {
        var created = await _organizationService.CreateOrganizationAsync(request);
        return CreatedAtAction(nameof(GetOrganizationById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateOrganization(Guid id, [FromBody] OrganizationRequest request)
    {
        var updated = await _organizationService.UpdateOrganizationAsync(id, request);
        if (updated is null)
        {
            _logger.LogWarning("Organization with id {Id} not found for update.", id);
            return NotFound();
        }
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrganization(Guid id)
    {
        var deleted = await _organizationService.DeleteOrganizationAsync(id);
        if (!deleted)
        {
            _logger.LogWarning("Organization with id {Id} not found for deletion.", id);
            return NotFound();
        }
        return NoContent();
    }
}

