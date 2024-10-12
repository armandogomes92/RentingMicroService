using Refit;

namespace MotorcycleService.Application.Interfaces;

public interface IRentalService
{
    [Get("/aluguel/validar-locacao")]
    Task<bool> CheckMotorcycleIsRenting(string motoId);
}