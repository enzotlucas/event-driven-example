using Example.EventDriven.Domain.Gateways.Event;
using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Application.Request.UpdateRequest.Boundaries
{
    [ExcludeFromCodeCoverage]
    public sealed class UpdateRequestStatusEvent : BaseEvent<UpdateRequestStatusRequest>
    {
        public override string OperationName => nameof(UpdateRequestStatusEvent);
    }
}
