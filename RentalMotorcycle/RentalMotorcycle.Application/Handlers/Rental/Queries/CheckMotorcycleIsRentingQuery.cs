using RentalMotorcycle.Application.Handlers.CommonResources;

namespace RentalMotorcycle.Application.Handlers.Rental.Queries;

public class CheckMotorcycleIsRentingQuery : Query
{
    public string Identificador { get; set; }
}