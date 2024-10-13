using RabbitMQ.Client;

namespace NotificationService.Infrastructure.Messaging;
public class RabbitMQClient
{
    private readonly RabbitMQConfiguration _config;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQClient(RabbitMQConfiguration config)
    {
        _config = config;

        var factory = new ConnectionFactory()
        {
            HostName = _config.Host
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: _config.QueueYear2024,
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

        _channel.QueueDeclare(queue: _config.QueueRegistered,
                      durable: false,
                      exclusive: false,
                      autoDelete: false,
                      arguments: null);

        _channel.QueueDeclare(queue: _config.QueueTotalPrice,
                      durable: false,
                      exclusive: false,
                      autoDelete: false,
                      arguments: null);

    }
    public IModel CreateModel()
    {
        return _connection.CreateModel();
    }
    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}