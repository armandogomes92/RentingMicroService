using MotorcycleService.Domain.Resources;
using MotorcycleService.Infrastructure.Logging;
using Microsoft.Extensions.Logging;
using MotorcycleService.Application.Handlers.CommonResources;
using MotorcycleService.Application.Interfaces;

namespace MotorcycleService.Application.Handlers.Motorcycle.Queries;

public class GetMotorcycleByIdHandler : QueryHandler<GetMotorcycleByIdQuery>
{
    private readonly IMotorcycleService _motorcycleService;
    private readonly ILogger<GetMotorcycleByIdHandler> _logger;

    private const string Name = nameof(GetMotorcycleByIdHandler);

    public GetMotorcycleByIdHandler(IMotorcycleService motorcycleService, ILogger<GetMotorcycleByIdHandler> logger)
    {
        _motorcycleService = motorcycleService;
        _logger = logger;
    }

    public override async Task<Response> Handle(GetMotorcycleByIdQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogMessages.Start(Name));

        var result = await _motorcycleService.GetMotorcycleByIdAsync(query.Id);

        if (result == null)
        {
            _logger.LogInformation(LogMessages.Finished(Name));

            return new Response { Content = new { Mensagem = Messages.MotorcycleNotFound } };
        }
        _logger.LogInformation(LogMessages.Finished(Name));

        return new Response { Content = result };
    }
}