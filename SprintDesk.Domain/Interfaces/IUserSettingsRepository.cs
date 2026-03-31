using CaseFlow.Domain.Entity;

namespace CaseFlow.Domain.Interfaces;

public interface IUserSettingsRepository
{
    Task<UserSettings?> GetByUserIdAsync(Guid userId);
    Task<UserSettings> CreateAsync(UserSettings settings);
    Task<UserSettings?> UpdateAsync(UserSettings settings);
}
