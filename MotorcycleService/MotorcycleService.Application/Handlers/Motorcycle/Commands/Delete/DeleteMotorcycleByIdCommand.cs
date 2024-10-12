using MotorcycleService.Application.Handlers.CommonResources;

namespace MotorcycleService.Application.Handlers.Motorcycle.Commands.Delete;

public class DeleteMotorcycleByIdCommand : Command
{
    public string Id { get; set; }
}