using MotorcycleService.Domain.Resources;
using MotorcycleService.Infrastructure.Logging;
using Microsoft.Extensions.Logging;
using MotorcycleService.Application.Handlers.CommonResources;
using MotorcycleService.Application.Interfaces;

namespace MotorcycleService.Application.Handlers.Motorcycle.Commands.Update;

public class UpdateMotorcyclePlateHandler : CommandHandler<UpdateMotorcycleCommand>
{
    private readonly IMotorcycleService _motorcycleService;
    private readonly ILogger<UpdateMotorcyclePlateHandler> _logger;

    private const string Name = nameof(UpdateMotorcyclePlateHandler);
    public UpdateMotorcyclePlateHandler(IMotorcycleService motorcycleService, ILogger<UpdateMotorcyclePlateHandler> logger)
    {
        _motorcycleService = motorcycleService;
        _logger = logger;
    }

    public override async Task<Response> Handle(UpdateMotorcycleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogMessages.Start(Name));

        var existingplate = await _motorcycleService.GetMotorcyclesByPlateAsync(command.Placa);

        if(existingplate != null)
        {
            _logger.LogWarning(LogMessages.Finished(Name));
            return new Response { Content = new { Messagem = Messages.PlateAlreadyHasRegistration } };
        }

        var result = await _motorcycleService.UpdateMotorcycleByIdAsync(command, cancellationToken);

        _logger.LogInformation(LogMessages.Finished(Name));

        return new Response { Content = result };
    }
}