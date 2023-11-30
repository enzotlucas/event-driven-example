namespace Example.EventDriven.Domain.Gateways.Event
{
    public interface IEventSenderManager : IDisposable
    {
        Task<Guid> Send<TEventRequest>(BaseEvent<TEventRequest> genericEvent, CancellationToken cancellationToken);
    }
}
