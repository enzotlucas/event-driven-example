using EasyNetQ;
using Example.EventDriven.Domain.Gateways.Event;
using Example.EventDriven.Infrastructure.Event.Configurations;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Infrastructure.Event
{
    [ExcludeFromCodeCoverage]
    public sealed class RabbitMqConsumerManager : BaseRabbitMqManager, IEventConsumerManager
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

        public void Dispose()
        {
            ExecuteDispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
