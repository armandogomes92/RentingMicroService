using Microsoft.Extensions.Hosting;
using NotificationService.Infrastructure.Messaging;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using NotificationService.Domain.Model;
using NotificationService.Service.Services;

namespace NotificationService.Service.Consumers;

public class ConsumerTotalPrice : BackgroundService
{
    private readonly RabbitMQClient _rabbitMQClient;
    private readonly RabbitMQConfiguration _config;
    private readonly IModel _channel;
    private readonly MongoService _mongoService;

    public ConsumerTotalPrice(RabbitMQClient rabbitMQClient, RabbitMQConfiguration rabbitMQConfiguration, IModel channel, MongoService mongoDBService)
    {
        _rabbitMQClient = rabbitMQClient;
        _config = rabbitMQConfiguration;
        _channel = channel;
        _mongoService = mongoDBService;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var totalPrice = JsonSerializer.Deserialize<TotalPrice>(message);

            await _mongoService.AddTotalPriceAsync(totalPrice);

            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };
        _channel.BasicConsume(_config.QueueTotalPrice, false, consumer);

        return Task.CompletedTask;
    }
}