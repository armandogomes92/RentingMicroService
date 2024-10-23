using Microsoft.Extensions.Logging;
using RentalMotorcycle.Application.Handlers.CommonResources;
using RentalMotorcycle.Application.Interfaces;
using RentalMotorcycle.Domain.Resources;
using RentalMotorcycle.Infrastructure.Logging;

namespace RentalMotorcycle.Application.Handlers.Rental.Commands.Create;

public class CreateRentalRegistryHandler : CommandHandler<CreateRentalRegistryCommand>
{
    private readonly ILogger<CreateRentalRegistryHandler> _logger;
    private readonly IRentalService _deliveryManService;

    private const string NameOfClass = nameof(CreateRentalRegistryHandler);
    public CreateRentalRegistryHandler(ILogger<CreateRentalRegistryHandler> logger, IRentalService deliveryManService)
    {
        _logger = logger;
        _deliveryManService = deliveryManService;
    }

    public override async Task<Response> Handle(CreateRentalRegistryCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogMessages.Start($"{NameOfClass}"));

        var checkCnhType = await _deliveryManService.CheckDeliveryManCnh(command.EntregadorId);
        var checkMotorcycleIsRenting = await _deliveryManService.CheckMotorcycleIsRenting(command.MotoId);

        if (!checkCnhType || checkMotorcycleIsRenting)
        {
            _logger.LogWarning(LogMessages.Finished($"{NameOfClass}"));

            return new Response
            {
                Content = !checkCnhType ? new { Mensagem = Messages.InvalidCnh } :
                                         new { Mensagem = Messages.MotorcycleIsRenting }
            };
        }
        command.DataInicio.AddDays(1);
        var result = await _deliveryManService.CreateRentalRegistry(command);

        if (!result)
        {
            _logger.LogError(LogMessages.Finished($"{NameOfClass}"));

            return new Response {Content = new { Mensagem = Messages.InvalidData } };
        }

        _logger.LogInformation(LogMessages.Finished(NameOfClass));

        return new Response { Content = result };
    }
}