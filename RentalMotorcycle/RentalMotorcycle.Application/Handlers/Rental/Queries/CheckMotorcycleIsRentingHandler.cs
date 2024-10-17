using Microsoft.Extensions.Logging;
using RentalMotorcycle.Application.Handlers.CommonResources;
using RentalMotorcycle.Application.Interfaces;
using RentalMotorcycle.Domain.Resources;
using RentalMotorcycle.Infrastructure.Logging;

namespace RentalMotorcycle.Application.Handlers.Rental.Queries;

public class CheckMotorcycleIsRentingHandler : QueryHandler<CheckMotorcycleIsRentingQuery>
{
    private readonly IRentalService _rentalService;
    private readonly ILogger<GetRentalRegistryByIdHandler> _logger;

    private const string NameOfClass = nameof(CheckMotorcycleIsRentingHandler);
    public CheckMotorcycleIsRentingHandler(IRentalService rentalRegistryRepository, ILogger<GetRentalRegistryByIdHandler> logger)
    {
        _rentalService = rentalRegistryRepository;
        _logger = logger;
    }
    public override async Task<Response> Handle(CheckMotorcycleIsRentingQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogMessages.Start($"{NameOfClass}"));

        var result = await _rentalService.CheckMotorcycleIsRenting(query.Identificador);
       
        _logger.LogError(LogMessages.Finished(NameOfClass));
        return new Response { Content = result };
    }
}
