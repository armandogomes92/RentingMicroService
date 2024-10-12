using MotorcycleService.Application.Handlers.CommonResources;

namespace MotorcycleService.Application.Handlers.Motorcycle.Queries;

public class GetMotorcycleByIdQuery : Query
{
    public string Id { get; set; }
}