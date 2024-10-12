using RentalMotorcycle.Application.Handlers.CommonResources;

namespace RentalMotorcycle.Application.Handlers.Rental.Queries;

public class GetRentalRegistryByIdQuery : Query
{
    public int Identificador { get; set; }
}