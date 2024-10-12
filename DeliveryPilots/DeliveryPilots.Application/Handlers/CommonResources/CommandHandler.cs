using DeliveryPilots.Domain.Resources;
using MediatR;

namespace DeliveryPilots.Application.Handlers.CommonResources;

public abstract class CommandHandler<TCommand> : IRequestHandler<TCommand, Response> where TCommand : IRequest<Response>
{
    public abstract Task<Response> Handle(TCommand command, CancellationToken cancellationToken);
}
