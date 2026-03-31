using CaseFlow.Domain.Entity;
using CaseFlow.Domain.Interfaces;
using CaseFlow.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.Infrastructure.Repository;

public class NotificationRepository : INotificationRepository
{
    private readonly AppDbContext _context;

    public NotificationRepository(AppDbContext context) => _context = context;

    public async Task<List<Notification>> GetByUserIdAsync(Guid userId)
        => await _context.Notifications.AsNoTracking().Where(n => n.UserId == userId).ToListAsync();

    public async Task<Notification?> GetByIdAsync(Guid id)
        => await _context.Notifications.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);

    public async Task<Notification> CreateAsync(Notification notification)
    {
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
        return notification;
    }

    public async Task<bool> MarkAsReadAsync(Guid id)
    {
        var existing = await _context.Notifications.FindAsync(id);
        if (existing is null) return false;

        existing.IsRead = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await _context.Notifications.FindAsync(id);
        if (existing is null) return false;

        _context.Notifications.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
