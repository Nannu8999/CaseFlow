using CaseFlow.Application.DTOs.UserSettings.Requests;
using CaseFlow.Application.DTOs.UserSettings.Responses;
using CaseFlow.Application.Interfaces;
using CaseFlow.Domain.Entity;
using CaseFlow.Domain.Interfaces;

namespace CaseFlow.Application.Services;

public class UserSettingsService : IUserSettingsService
{
    private readonly IUserSettingsRepository _repository;

    public UserSettingsService(IUserSettingsRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<UserSettingsResponse?> GetByUserIdAsync(Guid userId)
    {
        var settings = await _repository.GetByUserIdAsync(userId);
        return settings is null ? null : MapToResponse(settings);
    }

    public async Task<UserSettingsResponse> CreateAsync(UserSettingsRequest request)
    {
        var settings = new UserSettings
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Theme = request.Theme,
            CreatedAt = DateTime.UtcNow
        };
        return MapToResponse(await _repository.CreateAsync(settings));
    }

    public async Task<UserSettingsResponse?> UpdateAsync(Guid userId, UserSettingsRequest request)
    {
        var settings = new UserSettings { UserId = userId, Theme = request.Theme };
        var updated = await _repository.UpdateAsync(settings);
        return updated is null ? null : MapToResponse(updated);
    }

    private static UserSettingsResponse MapToResponse(UserSettings s) => new()
    {
        Id = s.Id,
        UserId = s.UserId,
        Theme = s.Theme,
        CreatedAt = s.CreatedAt
    };
}
