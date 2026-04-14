using Asp.Versioning;
using CaseFlow.Application.DTOs.Client.Requests;
using CaseFlow.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IAuthService _authService;
    private readonly ILogger<ClientController> _logger;

    public ClientController(IClientService clientService, IAuthService authService, ILogger<ClientController> logger)
    {
        _clientService = clientService;
        _authService = authService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _clientService.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var client = await _clientService.GetByIdAsync(id);
        if (client is null) { _logger.LogWarning("Client {Id} not found.", id); return NotFound(); }
        return Ok(client);
    }

    [HttpGet("my-organization")]
    public async Task<IActionResult> GetByOrganization()
    {
        var organizationId = _authService.GetOrganizationIdFromToken(User);
        if (organizationId is null) return Unauthorized();
        return Ok(await _clientService.GetByOrganizationIdAsync(organizationId.Value));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClientRequest request)
    {
        var organizationId = _authService.GetOrganizationIdFromToken(User);
        if (organizationId is null) return Unauthorized();

        var created = await _clientService.CreateAsync(request, organizationId.Value);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ClientRequest request)
    {
        var organizationId = _authService.GetOrganizationIdFromToken(User);
        if (organizationId is null) return Unauthorized();

        var updated = await _clientService.UpdateAsync(id, request, organizationId.Value);
        if (updated is null) { _logger.LogWarning("Client {Id} not found for update.", id); return NotFound(); }
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _clientService.DeleteAsync(id);
        if (!deleted) { _logger.LogWarning("Client {Id} not found for deletion.", id); return NotFound(); }
        return NoContent();
    }
}
