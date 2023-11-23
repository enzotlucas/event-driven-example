namespace Example.EventDriven.Domain.Gateways.Event
{
    public abstract class GenericEvent<T>
    {
        public DateTime Timestamp { get; private set; }
        public abstract string OperationName { get; }
        public Guid RequestId { get; set; }
        public T Value { get; set; }
    }
}
