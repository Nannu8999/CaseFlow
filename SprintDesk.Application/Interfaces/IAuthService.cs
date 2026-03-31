using CaseFlow.Application.DTOs.Auth;
using System.Security.Claims;

namespace CaseFlow.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
    Guid? GetUserIdFromToken(ClaimsPrincipal user);
    Guid? GetOrganizationIdFromToken(ClaimsPrincipal user);
}
