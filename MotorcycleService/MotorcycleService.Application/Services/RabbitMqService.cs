using MotorcycleService.Application.Interfaces;
using MotorcycleService.Infrastructure.Messaging;

namespace MotorcycleService.Application.Services;

public class RabbitMqService : IRabbitMqService
{
    private readonly RabbitMQClient _rabbitMQClient;
    private readonly RabbitMQConfiguration _config;

    public RabbitMqService(RabbitMQClient rabbitMQClient, RabbitMQConfiguration config)
    {
        _rabbitMQClient = rabbitMQClient;
        _config = config;
    }

    public void PublishRegisteredMotorcycle<T>(T message)
    {
        _rabbitMQClient.SendMessage(message, _config.QueueRegistered);
    }

    public void SendYear2024Message<T>(T message)
    {
        _rabbitMQClient.SendMessage(message, _config.QueueYear2024);
    }
}