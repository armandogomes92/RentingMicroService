using RentalMotorcycle.Application.Handlers.Rental.Commands.Create;
using RentalMotorcycle.Application.Handlers.Rental.Commands.Update;
using RentalMotorcycle.Domain.Models;

namespace RentalMotorcycle.Application.Interfaces;

public interface IRentalService
{
    Task<bool> CreateRentalRegistry(CreateRentalRegistryCommand command);
    Task<Rental> GetRentalById(int id);
    Task<bool> CheckMotorcycleIsRenting(string identificador);
    Task<bool> EndRentalAsync(UpdateRentalRegistryCommand command);
    Task<bool> CheckDeliveryManCnh(string deliveryManId);
}