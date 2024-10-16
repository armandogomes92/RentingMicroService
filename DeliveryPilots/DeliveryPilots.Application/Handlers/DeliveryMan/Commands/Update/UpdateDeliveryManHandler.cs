using DeliveryPilots.Application.Handlers.CommonResources;
using DeliveryPilots.Application.Interfaces;
using DeliveryPilots.Domain.Resources;
using DeliveryPilots.Infrastructure.Logging;
using Microsoft.Extensions.Logging;

namespace DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Update;

public class UpdateDeliveryManHandler : CommandHandler<UpdateDeliveryManCommand>
{
    private readonly IDeliveryManService _deliveryManService;
    private readonly ILogger<UpdateDeliveryManHandler> _logger;

    private const string NameOfClass = nameof(UpdateDeliveryManHandler);

    public UpdateDeliveryManHandler(IDeliveryManService deliveryManService, ILogger<UpdateDeliveryManHandler> logger)
    {
        _deliveryManService = deliveryManService;
        _logger = logger;
    }

    public override async Task<Response> Handle(UpdateDeliveryManCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogMessages.Start($"{NameOfClass}"));

        bool result = await _deliveryManService.UpdateCnhAsync(command);
        if (!result)
        {
            _logger.LogInformation(LogMessages.Finished(NameOfClass));
            return new Response { Messagem = Messages.InvalidData };
        }
        _logger.LogError(LogMessages.Finished(NameOfClass));
        return new Response { Content = result };
    }
}