namespace Example.EventDriven.Domain.Gateways.Event
{
    public interface IEventManager
    {
        Task<Guid> SendEvent(GenericEvent genericEvent, CancellationToken cancellationToken);
    }
}
