using DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Create;
using DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Update;

namespace DeliveryPilots.Application.Interfaces;

public interface IDeliveryManService
{
    Task<bool> CreateDeliveryMan(CreateDeliveryManCommand command);
    Task<bool> UpdateCnhAsync(UpdateDeliveryManCommand command);
    Task<string> GetCnhCategoryAsync(string id);
}