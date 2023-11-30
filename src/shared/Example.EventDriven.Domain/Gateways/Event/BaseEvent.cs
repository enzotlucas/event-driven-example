using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Domain.Gateways.Event
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseEvent<T>
    {
        public DateTime Timestamp { get; private set; }
        public abstract string OperationName { get; }
        public Guid RequestId { get; set; }
        public T Value { get; set; }
    }
}
