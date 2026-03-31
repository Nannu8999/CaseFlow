using CaseFlow.Application.DTOs.Notifications.Requests;
using CaseFlow.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;
    private readonly IAuthService _authService;
    private readonly ILogger<NotificationController> _logger;

    public NotificationController(INotificationService notificationService, IAuthService authService, ILogger<NotificationController> logger)
    {
        _notificationService = notificationService;
        _authService = authService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyNotifications()
    {
        var userId = _authService.GetUserIdFromToken(User);
        if (userId is null) return Unauthorized();
        return Ok(await _notificationService.GetByUserIdAsync(userId.Value));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NotificationRequest request)
    {
        var created = await _notificationService.CreateAsync(request);
        return StatusCode(201, created);
    }

    [HttpPatch("{id:guid}/read")]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        var result = await _notificationService.MarkAsReadAsync(id);
        if (!result) { _logger.LogWarning("Notification {Id} not found.", id); return NotFound(); }
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _notificationService.DeleteAsync(id);
        if (!deleted) { _logger.LogWarning("Notification {Id} not found for deletion.", id); return NotFound(); }
        return NoContent();
    }
}
