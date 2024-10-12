using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using DeliveryPilots.Domain.Resources;
using MediatR;

namespace DeliveryPilots.Application.Handlers.CommonResources
{
    [ExcludeFromCodeCoverage]
    public class Command : IRequest<Response>, IBaseRequest
    {
        protected long Timestamp { get; }

        protected string? TraceId { get; }

        protected string? ParentId { get; }

        protected Command()
        {
            Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            TraceId = Activity.Current?.Id;
            ParentId = Activity.Current?.ParentId;
        }
    }
}
