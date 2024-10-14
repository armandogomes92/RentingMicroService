using Refit;

namespace RentalMotorcycle.Application.Interfaces;

public interface IDeliveryManService
{
    [Get("/deliveryman/{id}")]
    Task<bool> GetCnhType(string id);
}
