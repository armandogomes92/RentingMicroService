using Refit;

namespace RentalMotorcycle.Application.Interfaces;

public interface IDeliveryManService
{
    [Get("/api/v1/deliveryman/{id}")]
    Task<bool> GetCnhType(string id);
}
