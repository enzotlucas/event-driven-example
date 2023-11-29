using Example.EventDriven.Domain.Gateways.Event;
using Example.EventDriven.Infrastructure.Event.Configurations;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Infrastructure.Event
{
    [ExcludeFromCodeCoverage]
    public sealed class RabbitMqSenderManager : BaseRabbitMqManager, IEventSenderManager
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
            ExecuteDispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
