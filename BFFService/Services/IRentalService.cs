using BFFService.Models;
using Refit;

namespace BFFService.Services;

public interface IRentalService
{
    [Post("/locacao")]
    Task<HttpResponseMessage> Post([Body] CreateRentalRegistryCommand command);

    [Get("/locacao/{id}")]
    Task<HttpResponseMessage> Get([AliasAs("id")] int id);

    [Put("/locacao/{id}/devolucao")]
    Task<HttpResponseMessage> Put([AliasAs("id")] string id, [Body] Locacao locacao);
}
