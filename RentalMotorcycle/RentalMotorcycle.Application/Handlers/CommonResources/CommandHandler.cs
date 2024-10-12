using MediatR;
using RentalMotorcycle.Domain.Resources;

namespace RentalMotorcycle.Application.Handlers.CommonResources;

public abstract class CommandHandler<TCommand> : IRequestHandler<TCommand, Response> where TCommand : IRequest<Response>
{
    public abstract Task<Response> Handle(TCommand command, CancellationToken cancellationToken);
}