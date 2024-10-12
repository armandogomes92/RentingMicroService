using System.ComponentModel;

namespace MotorcycleService.Domain.Resources;

public enum NotificationType
{
    [Description("ValidationError")]
    ValidationError,
    [Description("Success")]
    Success,
    [Description("Warn")]
    Warn,
    [Description("NotFound")]
    NotFound
}
