using Microsoft.Extensions.Hosting;
using NotificationService.Infrastructure.Messaging;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using NotificationService.Service.Services;

namespace NotificationService.Service.Consumers;

public class ConsumerRegisteredMotorcycle : BackgroundService
{
    private readonly RabbitMQClient _rabbitMQClient;
    private readonly RabbitMQConfiguration _config;
    private readonly IModel _channel;
    private readonly MongoService _mongoService;

    public ConsumerRegisteredMotorcycle(RabbitMQClient rabbitMQClient, RabbitMQConfiguration rabbitMQConfiguration, IModel channel, MongoService mongoService)
    {
        _rabbitMQClient = rabbitMQClient;
        _config = rabbitMQConfiguration;
        _channel = channel;
        _mongoService = mongoService;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            await _mongoService.AddAdminNotificationAsync(message);

            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };
        _channel.BasicConsume(_config.QueueRegistered, false, consumer);

        return Task.CompletedTask;
    }
}
