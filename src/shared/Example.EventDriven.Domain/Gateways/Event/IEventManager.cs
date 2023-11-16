namespace Example.EventDriven.Domain.Gateways.Event
{
    public interface IEventManager
    {
        Task<Guid> SendEvent(BaseEvent genericEvent, CancellationToken cancellationToken);
    }
}
