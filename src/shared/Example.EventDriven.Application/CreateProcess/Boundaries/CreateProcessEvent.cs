using Example.EventDriven.Domain.Gateways.Event;

namespace Example.EventDriven.Application.CreateProcess.Boundaries
{
    public class CreateProcessEvent : GenericEvent<CreateProcessRequest>
    {
        public override string OperationName => nameof(CreateProcessEvent);
    }
}
