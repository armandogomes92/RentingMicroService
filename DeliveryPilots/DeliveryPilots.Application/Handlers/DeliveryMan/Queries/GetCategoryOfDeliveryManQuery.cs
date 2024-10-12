using DeliveryPilots.Application.Handlers.CommonResources;
using System.ComponentModel.DataAnnotations;

namespace DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Create;

public class GetCategoryOfDeliveryManQuery : Query
{
    [Required]
    public string Identificador { get; set; }
}