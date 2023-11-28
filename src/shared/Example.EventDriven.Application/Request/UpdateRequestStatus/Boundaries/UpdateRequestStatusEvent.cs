using Example.EventDriven.Domain.Gateways.Event;

namespace Example.EventDriven.Application.Request.UpdateRequest.Boundaries
{
    public sealed class UpdateRequestStatusEvent : GenericEvent<UpdateRequestStatusRequest>
    {
        public override string OperationName => nameof(UpdateRequestStatusEvent);
    }
}
