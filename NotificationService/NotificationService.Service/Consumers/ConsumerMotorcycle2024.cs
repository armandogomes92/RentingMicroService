using Microsoft.Extensions.Hosting;
using NotificationService.Infrastructure.Messaging;
using NotificationService.Service.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace NotificationService.Service.Consumers;

public class ConsumerMotorcycle2024 : BackgroundService
{
    private readonly RabbitMQClient _rabbitMQClient;
    private readonly RabbitMQConfiguration _config;
    private readonly IModel _channel;
    private readonly MongoService _mongoService;

    public ConsumerMotorcycle2024(RabbitMQClient rabbitMQClient, RabbitMQConfiguration rabbitMQConfiguration, IModel channel, MongoService mongoService)
    {
        _rabbitMQClient = rabbitMQClient;
        _config = rabbitMQConfiguration;
        _channel = channel;
        _mongoService = mongoService;
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
        _channel.BasicConsume(_config.QueueYear2024, false, consumer);

        return Task.CompletedTask;
    }
}
