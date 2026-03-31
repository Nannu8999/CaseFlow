using CaseFlow.Domain.Entity;

namespace CaseFlow.Domain.Interfaces;

public interface INotificationRepository
{
    Task<List<Notification>> GetByUserIdAsync(Guid userId);
    Task<Notification?> GetByIdAsync(Guid id);
    Task<Notification> CreateAsync(Notification notification);
    Task<bool> MarkAsReadAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);
}
