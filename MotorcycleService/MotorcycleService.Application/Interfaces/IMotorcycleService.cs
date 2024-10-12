using MotorcycleService.Application.Handlers.Motorcycle.Commands.Create;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Delete;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Update;
using MotorcycleService.Application.Handlers.Motorcycle.Queries;
using MotorcycleService.Domain.Models;

namespace MotorcycleService.Application.Interfaces;
public interface IMotorcycleService
{
    Task<bool> CreateMotorcycleAsync(CreateMotorcycleCommand command, CancellationToken cancellationToken);
    Task<IEnumerable<Motorcycle>> GetMotorcyclesAsync();
    Task<bool> UpdateMotorcycleByIdAsync(UpdateMotorcycleCommand command, CancellationToken cancelationToken);
    Task<Motorcycle> GetMotorcycleByIdAsync(GetMotorcycleByIdQuery Query);
    Task<bool> DeleteMotorcycleByIdAsync(DeleteMotorcycleByIdCommand command, CancellationToken cancelationToken);
}