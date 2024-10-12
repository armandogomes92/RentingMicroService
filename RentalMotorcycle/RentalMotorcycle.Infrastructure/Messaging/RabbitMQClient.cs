using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RentalMotorcycle.Infrastructure.Messaging;
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
            HostName = _config.Host,
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _config.QueueTotalPrice,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
    }

    public void SendMessage<T>(T message)
    {
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        _channel.BasicPublish(exchange: "",
                             routingKey: _config.QueueTotalPrice,
                             basicProperties: null,
                             body: body);
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}