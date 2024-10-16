using MotorcycleService.Domain.Resources;
using MotorcycleService.Infrastructure.Logging;
using Microsoft.Extensions.Logging;
using MotorcycleService.Application.Handlers.CommonResources;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Update;
using MotorcycleService.Application.Interfaces;

namespace MotorcycleService.Application.Handlers.Motorcycle.Queries;

public class GetMotorcyclesHandler : QueryHandler<GetMotorcyclesQuery>
{
    private readonly IMotorcycleService _motorcycleService;
    private readonly ILogger<GetMotorcyclesHandler> _logger;

    public GetMotorcyclesHandler(IMotorcycleService motorcycleService, ILogger<GetMotorcyclesHandler> logger)
    {
        _motorcycleService = motorcycleService;
        _logger = logger;
    }
    public override async Task<Response> Handle(GetMotorcyclesQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogMessages.Start(nameof(UpdateMotorcyclePlateHandler)));

        var result = await _motorcycleService.GetMotorcyclesAsync(query);

        _logger.LogInformation(LogMessages.Finished(nameof(UpdateMotorcyclePlateHandler)));

        return new Response { Content = result };
    }
}