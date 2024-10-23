using Microsoft.Extensions.Logging;
using RentalMotorcycle.Application.Handlers.CommonResources;
using RentalMotorcycle.Application.Interfaces;
using RentalMotorcycle.Domain.Resources;
using RentalMotorcycle.Infrastructure.Logging;

namespace RentalMotorcycle.Application.Handlers.Rental.Commands.Update;

public class UpdateRentalRegistryHandler : CommandHandler<UpdateRentalRegistryCommand>
{
    private readonly IRentalService _deliveryManService;
    private readonly ILogger<UpdateRentalRegistryHandler> _logger;

    private const string NameOfClass = nameof(UpdateRentalRegistryHandler);

    public UpdateRentalRegistryHandler(IRentalService deliveryManService, ILogger<UpdateRentalRegistryHandler> logger)
    {
        _deliveryManService = deliveryManService;
        _logger = logger;
    }

    public override async Task<Response> Handle(UpdateRentalRegistryCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogMessages.Start($"{NameOfClass}"));

        var result = await _deliveryManService.EndRentalAsync(command);
        if (!result)
        {
            _logger.LogWarning(LogMessages.Finished(NameOfClass));
            return new Response { Content = new { Mensagem = Messages.InvalidData } };
        }
        _logger.LogInformation(LogMessages.Finished(NameOfClass));
        return new Response { Content = new { Mensagem = Messages.ReturnedDate } };
    }
}