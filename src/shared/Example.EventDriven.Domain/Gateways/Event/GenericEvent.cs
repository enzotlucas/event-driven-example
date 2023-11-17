namespace Example.EventDriven.Domain.Gateways.Event
{
    public abstract class GenericEvent
    {
        public abstract string OperationName { get; }
        public object Value { get; set; }
    }
}
