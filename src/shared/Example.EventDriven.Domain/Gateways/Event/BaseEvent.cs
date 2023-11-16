namespace Example.EventDriven.Domain.Gateways.Event
{
    public abstract class BaseEvent
    {
        public abstract string OperationName { get; }
        public object Value { get; set; }
    }
}
