using CaseFlow.Application.DTOs.Auth;
using Asp.Versioning;
using CaseFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);

        if (response is null)
        {
            _logger.LogWarning("Failed login attempt for email {Email}.", request.Email);
            return Unauthorized(new { message = "Invalid email or password." });
        }

        return Ok(response);
    }
}
