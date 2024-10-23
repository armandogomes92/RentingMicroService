using RentalMotorcycle.Application.Handlers.CommonResources;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RentalMotorcycle.Application.Handlers.Rental.Commands.Update;

public class UpdateRentalRegistryCommand : Command
{
    [Required]
    [JsonIgnore]
    public int Identificador { get; set; }
    [Required]
    [JsonPropertyName("data_devolucao")]
    public DateTime DataDevolucao { get; set; }
}