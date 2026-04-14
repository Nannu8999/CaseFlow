using Asp.Versioning;
using CaseFlow.Application.DTOs.User.Requests;
using CaseFlow.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
//[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, IAuthService authService, ILogger<UserController> logger)
    {
        _userService = userService;
        _authService = authService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = _authService.GetUserIdFromToken(User);
        if (userId is null) return Unauthorized();

        var user = await _userService.GetUserByIdAsync(userId.Value);
        if (user is null) return NotFound();

        return Ok(user);
    }

    [HttpGet("my-organization")]
    public async Task<IActionResult> GetMyOrganizationUsers()
    {
        var organizationId = _authService.GetOrganizationIdFromToken(User);
        if (organizationId is null) return Unauthorized();

        var users = await _userService.GetUsersByOrganizationAsync(organizationId.Value);
        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user is null)
        {
            _logger.LogWarning("User with id {Id} not found.", id);
            return NotFound();
        }
        return Ok(user);
    }

    [HttpGet("organization/{organizationId:guid}")]
    public async Task<IActionResult> GetUsersByOrganization(Guid organizationId)
    {
        var users = await _userService.GetUsersByOrganizationAsync(organizationId);
        return Ok(users);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
    {
        var created = await _userService.CreateUserAsync(request);
        return CreatedAtAction(nameof(GetUserById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserRequest request)
    {
        var updated = await _userService.UpdateUserAsync(id, request);
        if (updated is null)
        {
            _logger.LogWarning("User with id {Id} not found for update.", id);
            return NotFound();
        }
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var deleted = await _userService.DeleteUserAsync(id);
        if (!deleted)
        {
            _logger.LogWarning("User with id {Id} not found for deletion.", id);
            return NotFound();
        }
        return NoContent();
    }
}
