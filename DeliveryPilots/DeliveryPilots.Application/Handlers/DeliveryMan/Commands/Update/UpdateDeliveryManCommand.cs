using DeliveryPilots.Application.Handlers.CommonResources;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Update;

public class UpdateDeliveryManCommand : Command
{
    [Required, JsonIgnore]
    public string Identificador { get; set; }
    [Required]
    public byte[] ImagemCnh { get; set; }
}