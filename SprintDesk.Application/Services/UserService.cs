using CaseFlow.Application.DTOs.User.Requests;
using CaseFlow.Application.DTOs.User.Responses;
using CaseFlow.Application.Interfaces;
using CaseFlow.Domain.Entity;
using CaseFlow.Domain.Interfaces;

using System.Security.Cryptography;
using System.Text;

namespace CaseFlow.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<List<UserResponse>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return users.Select(MapToResponse).ToList();
    }

    public async Task<UserResponse?> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user is null ? null : MapToResponse(user);
    }

    public async Task<List<UserResponse>> GetUsersByOrganizationAsync(Guid organizationId)
    {
        var users = await _userRepository.GetByOrganizationIdAsync(organizationId);
        return users.Select(MapToResponse).ToList();
    }

    public async Task<UserResponse> CreateUserAsync(UserRequest request)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = HashPassword(request.Password),
            OrganizationId = request.OrganizationId,
            Role = request.Role,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _userRepository.CreateAsync(user);
        return MapToResponse(created);
    }

    public async Task<UserResponse?> UpdateUserAsync(Guid id, UserRequest request)
    {
        var user = new User
        {
            Id = id,
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = HashPassword(request.Password),
            OrganizationId = request.OrganizationId,
            Role = request.Role,
            UpdatedAt = DateTime.UtcNow
        };

        var updated = await _userRepository.UpdateAsync(user);
        return updated is null ? null : MapToResponse(updated);
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    private static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes).ToLower();
    }

    private static UserResponse MapToResponse(User user) => new()
    {
        Id = user.Id,
        FullName = user.FullName,
        Email = user.Email,
        OrganizationId = user.OrganizationId,
        Role = user.Role,
        CreatedAt = user.CreatedAt,
        UpdatedAt = user.UpdatedAt
    };
}
