namespace MotorcycleService.Domain.Resources;

public record struct Notification(string Message, string? PropertyName = null, NotificationType? Type = NotificationType.ValidationError);
