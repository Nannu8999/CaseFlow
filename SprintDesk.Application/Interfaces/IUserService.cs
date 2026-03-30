using CaseFlow.Application.DTOs.User.Requests;
using CaseFlow.Application.DTOs.User.Responses;

namespace CaseFlow.Application.Interfaces;

public interface IUserService
{
    Task<List<UserResponse>> GetAllUsersAsync();
    Task<UserResponse?> GetUserByIdAsync(Guid id);
    Task<List<UserResponse>> GetUsersByOrganizationAsync(Guid organizationId);
    Task<UserResponse> CreateUserAsync(UserRequest request);
    Task<UserResponse?> UpdateUserAsync(Guid id, UserRequest request);
    Task<bool> DeleteUserAsync(Guid id);
}
