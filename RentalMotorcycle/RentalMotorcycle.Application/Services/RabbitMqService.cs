using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using RentalMotorcycle.Application.Interfaces;
using RentalMotorcycle.Infrastructure.Messaging;

namespace RentalMotorcycle.Application.Services;

public class RabbitMqService : IRabbitMqService
{
    private readonly RabbitMQClient _rabbitMQClient;
    private readonly RabbitMQConfiguration _config;

    public RabbitMqService(RabbitMQClient rabbitMQClient, RabbitMQConfiguration config)
    {
        _rabbitMQClient = rabbitMQClient;
        _config = config;
    }

    public void PublishTotalPriceOfRental<T>(T message)
    {
        _rabbitMQClient.SendMessage(message);
    }
}