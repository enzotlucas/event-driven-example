using EasyNetQ;
using Example.EventDriven.Domain.Gateways.Event;
using Example.EventDriven.Infrastructure.Event.Configurations;
using Microsoft.Extensions.Configuration;

namespace Example.EventDriven.Infrastructure.Event
{
    public class RabbitMqConsumerManager : BaseRabbitMqManager, IEventConsumerManager
    {
        public RabbitMqConsumerManager(IConfiguration configuration) : base(configuration)
        {
            
        }

        public async Task SubscribeAsync<TEvent, TEventRequest>(string subscriptionId,
                                          Func<TEvent, CancellationToken, Task> onMessage,
                                          Action<ISubscriptionConfiguration> configuration,
                                          CancellationToken cancellationToken)
                                          where TEvent : GenericEvent<TEventRequest>

        {
            TryConnect();

            await MessageBus.PubSub.SubscribeAsync(subscriptionId, onMessage, configuration, cancellationToken);
        }
    }
}
