using CaseFlow.Domain.Entity;
using CaseFlow.Domain.Interfaces;
using CaseFlow.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.Infrastructure.Repository;

public class UserSettingsRepository : IUserSettingsRepository
{
    private readonly AppDbContext _context;

    public UserSettingsRepository(AppDbContext context) => _context = context;

    public async Task<UserSettings?> GetByUserIdAsync(Guid userId)
        => await _context.UserSettings.AsNoTracking().FirstOrDefaultAsync(s => s.UserId == userId);

    public async Task<UserSettings> CreateAsync(UserSettings settings)
    {
        _context.UserSettings.Add(settings);
        await _context.SaveChangesAsync();
        return settings;
    }

    public async Task<UserSettings?> UpdateAsync(UserSettings settings)
    {
        var existing = await _context.UserSettings.FirstOrDefaultAsync(s => s.UserId == settings.UserId);
        if (existing is null) return null;

        existing.Theme = settings.Theme;
        await _context.SaveChangesAsync();
        return existing;
    }
}
