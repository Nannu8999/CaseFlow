using CaseFlow.Application.DTOs.UserSettings.Requests;
using CaseFlow.Application.DTOs.UserSettings.Responses;

namespace CaseFlow.Application.Interfaces;

public interface IUserSettingsService
{
    Task<UserSettingsResponse?> GetByUserIdAsync(Guid userId);
    Task<UserSettingsResponse> CreateAsync(UserSettingsRequest request);
    Task<UserSettingsResponse?> UpdateAsync(Guid userId, UserSettingsRequest request);
}
