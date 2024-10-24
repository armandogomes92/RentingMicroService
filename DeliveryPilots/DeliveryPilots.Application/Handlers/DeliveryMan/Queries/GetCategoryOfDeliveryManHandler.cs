﻿using DeliveryPilots.Application.Handlers.CommonResources;
using DeliveryPilots.Application.Interfaces;
using DeliveryPilots.Domain.Resources;
using DeliveryPilots.Infrastructure.Logging;
using Microsoft.Extensions.Logging;

namespace DeliveryPilots.Application.Handlers.DeliveryMan.Queries;

public class GetCategoryOfDeliveryManHandler : QueryHandler<GetCategoryOfDeliveryManQuery>
{
    private readonly ILogger<GetCategoryOfDeliveryManHandler> _logger;
    private readonly IDeliveryManService _deliveryManService;

    private const string NameOfClass = nameof(GetCategoryOfDeliveryManHandler);
    public GetCategoryOfDeliveryManHandler(ILogger<GetCategoryOfDeliveryManHandler> logger, IDeliveryManService deliveryManService)
    {
        _logger = logger;
        _deliveryManService = deliveryManService;
    }

    public override async Task<Response> Handle(GetCategoryOfDeliveryManQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogMessages.Start($"{NameOfClass}"));

        var result = await _deliveryManService.GetCnhCategoryAsync(query.Identificador);

        if (string.IsNullOrEmpty(result))
        {
            _logger.LogError(LogMessages.Finished($"{NameOfClass}"));

            return new Response { Content = 'N' };
        }

        _logger.LogInformation(LogMessages.Finished(NameOfClass));

        return new Response { Content = result };
    }
}