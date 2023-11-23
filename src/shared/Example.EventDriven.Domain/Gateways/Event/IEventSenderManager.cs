namespace Example.EventDriven.Domain.Gateways.Event
{
    public interface IEventSenderManager : IDisposable
    {
        Task<Guid> Send<TEventRequest>(GenericEvent<TEventRequest> genericEvent, CancellationToken cancellationToken);
    }
}
