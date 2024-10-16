using MotorcycleService.Application.Handlers.CommonResources;

namespace MotorcycleService.Application.Handlers.Motorcycle.Queries
{
    public class GetMotorcyclesQuery : Query
    {
        public string? Placa { get; set; }
    }
}
