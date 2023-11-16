using Example.EventDriven.Domain.Gateways.Event;

namespace Example.EventDriven.Infrastructure.Event
{
    public class RabbitMqManager : IEventManager
    {
        public Task<Guid> SendEvent(BaseEvent genericEvent, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
