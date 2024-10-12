namespace NotificationService.Infrastructure.MongoDB;

public class MongoConfig
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string AdminNotification { get; set; }
    public string UserNotification { get; set; }
}
