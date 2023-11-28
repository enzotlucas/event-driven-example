using Example.EventDriven.Domain.Entitites;
using Example.EventDriven.Domain.Gateways.Event;

namespace Example.EventDriven.Application.Request.UpdateRequest.Boundaries
{
    public sealed class UpdateRequestStatusEvent : GenericEvent<RequestEntity<ProcessEntity>>
    {
        public override string OperationName => nameof(UpdateRequestStatusEvent);
    }
}
