using Refit;

namespace RentalMotorcycle.Application.Interfaces;

public interface IDeliveryManService
{
    [Get("/entregadores/{id}/cnh-tipo")]
    Task<string> GetCnhType([AliasAs("id")]string id);
}
