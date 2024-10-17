using Microsoft.Extensions.Logging;
using RentalMotorcycle.Application.Handlers.CommonResources;
using RentalMotorcycle.Application.Interfaces;
using RentalMotorcycle.Domain.Resources;
using RentalMotorcycle.Infrastructure.Logging;

namespace RentalMotorcycle.Application.Handlers.Rental.Queries;

public class GetRentalRegistryByIdHandler : QueryHandler<GetRentalRegistryByIdQuery>
{
    private readonly IRentalService _rentalRegistryRepository;
    private readonly ILogger<GetRentalRegistryByIdHandler> _logger;

    private const string NameOfClass = nameof(GetRentalRegistryByIdHandler);

    public GetRentalRegistryByIdHandler(IRentalService rentalRegistryRepository, ILogger<GetRentalRegistryByIdHandler> logger)
    {
        _rentalRegistryRepository = rentalRegistryRepository;
        _logger = logger;
    }
    public override async Task<Response> Handle(GetRentalRegistryByIdQuery command, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogMessages.Start($"{NameOfClass}"));

        var result = await _rentalRegistryRepository.GetRentalById(command.Identificador);

        _logger.LogError(LogMessages.Finished(NameOfClass));
        return new Response { Content = result };
    }
}