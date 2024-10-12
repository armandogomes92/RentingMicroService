namespace MotorcycleService.Application.Interfaces;

public interface IRabbitMqService
{
    void PublishRegisteredMotorcycle<T>(T message);
    void SendYear2024Message<T>(T message);
}