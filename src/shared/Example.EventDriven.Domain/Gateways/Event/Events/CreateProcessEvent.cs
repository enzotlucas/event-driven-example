namespace Example.EventDriven.Domain.Gateways.Event.Events
{
    public class CreateProcessEvent : BaseEvent
    {
        public override string OperationName => nameof(CreateProcessEvent);
    }
}
