using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MotorcycleService.Infrastructure.Messaging;
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
    }

    public void DeclareQueue(string queueName)
    {
        _channel.QueueDeclare(queue: queueName,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
    }

    public void SendMessage<T>(T message, string queueName)
    {
        DeclareQueue(queueName);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        _channel.BasicPublish(exchange: "",
                             routingKey: queueName,
                             basicProperties: null,
                             body: body);
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}