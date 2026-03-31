using CaseFlow.Application.DTOs.Notifications.Requests;
using CaseFlow.Application.DTOs.Notifications.Responses;
using CaseFlow.Application.Interfaces;
using CaseFlow.Domain.Entity;
using CaseFlow.Domain.Interfaces;

namespace CaseFlow.Application.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _repository;

    public NotificationService(INotificationRepository repository) => _repository = repository;

    public async Task<List<NotificationResponse>> GetByUserIdAsync(Guid userId)
        => (await _repository.GetByUserIdAsync(userId)).Select(MapToResponse).ToList();

    public async Task<NotificationResponse> CreateAsync(NotificationRequest request)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Message = request.Message,
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        };
        return MapToResponse(await _repository.CreateAsync(notification));
    }

    public async Task<bool> MarkAsReadAsync(Guid id)
        => await _repository.MarkAsReadAsync(id);

    public async Task<bool> DeleteAsync(Guid id)
        => await _repository.DeleteAsync(id);

    private static NotificationResponse MapToResponse(Notification n) => new()
    {
        Id = n.Id,
        UserId = n.UserId,
        Title = n.Title,
        Message = n.Message,
        IsRead = n.IsRead,
        CreatedAt = n.CreatedAt
    };
}
