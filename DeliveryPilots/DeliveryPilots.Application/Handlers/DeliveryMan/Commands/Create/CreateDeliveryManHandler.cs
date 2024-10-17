using DeliveryPilots.Application.Handlers.CommonResources;
using DeliveryPilots.Application.Interfaces;
using DeliveryPilots.Domain.Resources;
using DeliveryPilots.Infrastructure.Logging;
using Microsoft.Extensions.Logging;

namespace DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Create;

public class CreateDeliveryManHandler : CommandHandler<CreateDeliveryManCommand>
{
    private readonly ILogger<CreateDeliveryManHandler> _logger;
    private readonly IDeliveryManService _deliveryManService;

    private const string NameOfClass = nameof(CreateDeliveryManHandler);
    public CreateDeliveryManHandler(ILogger<CreateDeliveryManHandler> logger, IDeliveryManService deliveryManService)
    {
        _logger = logger;
        _deliveryManService = deliveryManService;
    }

    public override async Task<Response> Handle(CreateDeliveryManCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogMessages.Start($"{NameOfClass}"));

        var result = await _deliveryManService.CreateDeliveryMan(command);

        _logger.LogInformation(LogMessages.Finished(NameOfClass));

        return new Response { Content = result };
    }
}