using Microsoft.Extensions.Logging;
using MotorcycleService.Application.Handlers.CommonResources;
using MotorcycleService.Application.Interfaces;
using MotorcycleService.Domain.Resources;
using MotorcycleService.Infrastructure.Logging;

namespace MotorcycleService.Application.Handlers.Motorcycle.Commands.Create;
public class CreateMotorcycleHandler : CommandHandler<CreateMotorcycleCommand>
{
    private readonly IMotorcycleService _motorcycleService;
    private readonly ILogger<CreateMotorcycleHandler> _logger;

    private const string Name = nameof(CreateMotorcycleHandler);

    public CreateMotorcycleHandler(IMotorcycleService motorcycleService, ILogger<CreateMotorcycleHandler> logger)
    {
        _motorcycleService = motorcycleService;
        _logger = logger;
    }

    public override async Task<Response> Handle(CreateMotorcycleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogMessages.Start(Name));

        var existplate = await _motorcycleService.GetMotorcyclesByPlateAsync(command.Placa);
        var existId = await _motorcycleService.GetMotorcycleByIdAsync(command.Identificador);

        if (existplate != null)
        {
            return new Response { Content = new { Mensagem = Messages.PlateAlreadyHasRegistration } };
        }
        else if(existId != null)
        {
            return new Response { Content = new { Mensagem = Messages.IdentifierAlreadyHasRegistration } };
        }
        else
        {
            var result = await _motorcycleService.CreateMotorcycleAsync(command, cancellationToken);

            _logger.LogInformation(LogMessages.Finished(Name));

            var response = new Response { Content = result };

            return response;
        }
    }
}