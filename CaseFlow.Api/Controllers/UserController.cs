using CaseFlow.Application.DTOs.User.Requests;
using CaseFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
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
