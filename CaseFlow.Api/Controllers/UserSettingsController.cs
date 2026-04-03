using Asp.Versioning;
using CaseFlow.Application.DTOs.UserSettings.Requests;
using CaseFlow.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
public class UserSettingsController : ControllerBase
{
    private readonly IUserSettingsService _settingsService;
    private readonly IAuthService _authService;
    private readonly ILogger<UserSettingsController> _logger;

    public UserSettingsController(IUserSettingsService settingsService, IAuthService authService, ILogger<UserSettingsController> logger)
    {
        _settingsService = settingsService;
        _authService = authService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetMySettings()
    {
        var userId = _authService.GetUserIdFromToken(User);
        if (userId is null) return Unauthorized();

        var settings = await _settingsService.GetByUserIdAsync(userId.Value);
        if (settings is null) return NotFound();
        return Ok(settings);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserSettingsRequest request)
    {
        var created = await _settingsService.CreateAsync(request);
        return StatusCode(201, created);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UserSettingsRequest request)
    {
        var userId = _authService.GetUserIdFromToken(User);
        if (userId is null) return Unauthorized();

        var updated = await _settingsService.UpdateAsync(userId.Value, request);
        if (updated is null) { _logger.LogWarning("UserSettings for user {UserId} not found.", userId); return NotFound(); }
        return Ok(updated);
    }
}
