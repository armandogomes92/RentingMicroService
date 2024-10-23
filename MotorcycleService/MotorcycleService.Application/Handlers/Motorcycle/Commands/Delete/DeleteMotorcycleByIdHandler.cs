using MotorcycleService.Domain.Resources;
using MotorcycleService.Infrastructure.Logging;
using Microsoft.Extensions.Logging;
using MotorcycleService.Application.Handlers.CommonResources;
using MotorcycleService.Application.Interfaces;

namespace MotorcycleService.Application.Handlers.Motorcycle.Commands.Delete;

public class DeleteMotorcycleByIdHandler : CommandHandler<DeleteMotorcycleByIdCommand>
{
    private readonly IMotorcycleService _motorcycleService;
    private readonly ILogger<DeleteMotorcycleByIdHandler> _logger;

    private const string Name = nameof(DeleteMotorcycleByIdHandler);

    public DeleteMotorcycleByIdHandler(IMotorcycleService motorcycleService, ILogger<DeleteMotorcycleByIdHandler> logger)
    {
        _motorcycleService = motorcycleService;
        _logger = logger;
    }

    public override async Task<Response> Handle(DeleteMotorcycleByIdCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogMessages.Start(Name));

        var result = await _motorcycleService.DeleteMotorcycleByIdAsync(command, cancellationToken);

        _logger.LogInformation(LogMessages.Finished(Name));

        return new Response { Content = result };
    }
}