using MotorcycleService.Application.Handlers.CommonResources;
using System.Text.Json.Serialization;

namespace MotorcycleService.Application.Handlers.Motorcycle.Commands.Update;

public class UpdateMotorcycleCommand : Command
{
    [JsonIgnore]
    public string? Identificador { get; set; }
    public string Placa { get; set; }
}