using BFFService.Models;
using Refit;

namespace BFFService.Services
{
    public interface IDeliveryManService
    {
        [Post("/entregadores")]
        Task<HttpResponseMessage> PostEntregadores([Body] Entregadores entregadores);

        [Post("/entregadores/{id}/cnh")]
        Task<HttpResponseMessage> PostCnh([AliasAs("id")] string id, [Body] byte[] imagemCnh);
    }
}
