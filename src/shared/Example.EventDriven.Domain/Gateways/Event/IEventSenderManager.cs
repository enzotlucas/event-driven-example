namespace Example.EventDriven.Domain.Gateways.Event
{
    public interface IEventSenderManager : IDisposable
    {
        Task<Guid> Send<TEvent, TEventRequest>(TEvent genericEvent, CancellationToken cancellationToken) where TEvent : BaseEvent<TEventRequest>;
    }
}
