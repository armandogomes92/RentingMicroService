using BFFService.Models;
using Refit;

namespace BFFService.Services
{
    public interface IDeliveryManService
    {
        [Post("/entregadores")]
        Task<HttpResponseMessage> PostEntregadores([Body] CreateDeliveryManCommand command);

        [Post("/entregadores/{id}/cnh")]
        Task<HttpResponseMessage> PostCnh([AliasAs("id")] string id, [Body] byte[] imagemCnh);
    }
}
