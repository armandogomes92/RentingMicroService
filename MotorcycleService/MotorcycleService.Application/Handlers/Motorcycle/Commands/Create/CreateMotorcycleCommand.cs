using MotorcycleService.Application.Handlers.CommonResources;

namespace MotorcycleService.Application.Handlers.Motorcycle.Commands.Create;
public class CreateMotorcycleCommand : Command
{
    public string Identificador { get; set; }
    public int Ano { get; set; }
    public string Modelo { get; set; }
    public string Placa { get; set; }
}