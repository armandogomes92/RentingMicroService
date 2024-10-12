using RentalMotorcycle.Application.Handlers.CommonResources;
using System.ComponentModel.DataAnnotations;

namespace RentalMotorcycle.Application.Handlers.Rental.Commands.Update;

public class UpdateRentalRegistryCommand : Command
{
    [Required]
    public int Identificador { get; set; }
    [Required]
    public DateTime DataDevolucao { get; set; }
}