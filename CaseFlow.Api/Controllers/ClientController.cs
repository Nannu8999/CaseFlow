using CaseFlow.Application.DTOs.Client.Requests;
using CaseFlow.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly ILogger<ClientController> _logger;

    public ClientController(IClientService clientService, ILogger<ClientController> logger)
    {
        _clientService = clientService;
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

    [HttpGet("organization/{organizationId:guid}")]
    public async Task<IActionResult> GetByOrganization(Guid organizationId)
        => Ok(await _clientService.GetByOrganizationIdAsync(organizationId));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClientRequest request)
    {
        var created = await _clientService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ClientRequest request)
    {
        var updated = await _clientService.UpdateAsync(id, request);
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
