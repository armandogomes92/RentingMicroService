using MediatR;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using DeliveryPilots.Domain.Resources;

namespace DeliveryPilots.Application.Handlers.CommonResources;

[ExcludeFromCodeCoverage]
public class Query : IRequest<Response>, IBaseRequest
{
    protected string? TraceId { get; }

    protected string? ParentId { get; }

    protected Query()
    {
        TraceId = Activity.Current?.Id;
        ParentId = Activity.Current?.ParentId;
    }
}