using DeliveryPilots.Application.Handlers.CommonResources;
using DeliveryPilots.Application.Interfaces;
using DeliveryPilots.Domain.Resources;
using DeliveryPilots.Infrastructure.Logging;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Create;

public class GetCategoryOfDeliveryManHandle : QueryHandler<GetCategoryOfDeliveryManQuery>
{
    private readonly ILogger<GetCategoryOfDeliveryManHandle> _logger;
    private readonly IDeliveryManService _deliveryManService;

    private const string NameOfClass = nameof(GetCategoryOfDeliveryManHandle);
    public GetCategoryOfDeliveryManHandle(ILogger<GetCategoryOfDeliveryManHandle> logger, IDeliveryManService deliveryManService)
    {
        _logger = logger;
        _deliveryManService = deliveryManService;
    }

    public override async Task<Response> Handle(GetCategoryOfDeliveryManQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogMessages.Start($"{NameOfClass}"));

        var result = await _deliveryManService.GetCnhCategoryAsync(query.Identificador);

        if (String.IsNullOrEmpty(result))
        {
            _logger.LogError(LogMessages.Finished($"{NameOfClass}"));

            return new Response { Messagem = Messages.InvalidData };
        }

        _logger.LogInformation(LogMessages.Finished(NameOfClass));

        return new Response { Content = result };
    }
}