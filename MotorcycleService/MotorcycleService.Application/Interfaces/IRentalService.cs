using Refit;

namespace MotorcycleService.Application.Interfaces;

public interface IRentalService
{
    [Get("/locacao/validar-locacao/{id}")]
    Task<bool> CheckMotorcycleIsRenting([AliasAs("id")] string id);
}