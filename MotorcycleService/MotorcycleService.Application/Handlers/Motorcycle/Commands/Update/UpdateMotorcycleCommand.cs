using MotorcycleService.Application.Handlers.CommonResources;

namespace MotorcycleService.Application.Handlers.Motorcycle.Commands.Update;

public class UpdateMotorcycleCommand : Command
{
    public string Identificador { get; set; }
    public string Placa { get; set; }
}