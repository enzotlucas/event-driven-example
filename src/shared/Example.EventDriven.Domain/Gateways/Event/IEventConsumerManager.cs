using EasyNetQ;

namespace Example.EventDriven.Domain.Gateways.Event
{
    public interface IEventConsumerManager : IDisposable
    {
        Task SubscribeAsync<TEvent, TEventRequest>(string subscriptionId,
                                          Func<TEvent, CancellationToken, Task> onMessage,
                                          Action<ISubscriptionConfiguration> configuration,
                                          CancellationToken cancellationToken)
                                          where TEvent : BaseEvent<TEventRequest>;
    }
}
