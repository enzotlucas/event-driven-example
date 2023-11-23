using Example.EventDriven.Domain.Gateways.Event;
using Example.EventDriven.Infrastructure.Event.Configurations;
using Microsoft.Extensions.Configuration;

namespace Example.EventDriven.Infrastructure.Event
{
    public class RabbitMqSenderManager : BaseRabbitMqManager, IEventSenderManager
    {
        public RabbitMqSenderManager(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Guid> Send<TEventRequest>(GenericEvent<TEventRequest> genericEvent, CancellationToken cancellationToken)
        {
            if(genericEvent.RequestId == Guid.Empty)            
                genericEvent.RequestId = Guid.NewGuid();            

            TryConnect();

            await MessageBus.PubSub.PublishAsync(genericEvent, configure => configure.WithTopic(genericEvent.OperationName), cancellationToken);

            return genericEvent.RequestId;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                MessageBus.Dispose();
            }
        }
    }
}
