using MotorcycleService.Domain.Models;

namespace MotorcycleService.Infrastructure.Interfaces;

public interface IMotorcycleRepository
{
    Task<bool> AddMotorcycleAsync(Motorcycle moto);
    Task<List<Motorcycle>> GetAllMotorcyclesAsync(string? Placa);
    Task<bool> UpdateMotorcycleByIdAsync(Motorcycle moto);
    Task<Motorcycle> GetMotorcycleByIdAsync(string id);
    Task<bool> DeleteMotorcycleByIdAsync(Motorcycle moto);
}