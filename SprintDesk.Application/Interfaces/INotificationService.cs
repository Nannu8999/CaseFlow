using CaseFlow.Application.DTOs.Notifications.Requests;
using CaseFlow.Application.DTOs.Notifications.Responses;

namespace CaseFlow.Application.Interfaces;

public interface INotificationService
{
    Task<List<NotificationResponse>> GetByUserIdAsync(Guid userId);
    Task<NotificationResponse> CreateAsync(NotificationRequest request);
    Task<bool> MarkAsReadAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);
}
