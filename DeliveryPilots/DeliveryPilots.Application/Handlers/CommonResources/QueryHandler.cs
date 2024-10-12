using DeliveryPilots.Domain.Resources;
using MediatR;

namespace DeliveryPilots.Application.Handlers.CommonResources;

public abstract class QueryHandler<TQuery> : IRequestHandler<TQuery, Response> where TQuery : Query
{
    public abstract Task<Response> Handle(TQuery query, CancellationToken cancellationToken);
}