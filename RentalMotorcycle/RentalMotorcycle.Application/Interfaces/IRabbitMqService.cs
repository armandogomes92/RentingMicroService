namespace RentalMotorcycle.Application.Interfaces;

public interface IRabbitMqService
{
    void PublishTotalPriceOfRental<T>(T message);
}
