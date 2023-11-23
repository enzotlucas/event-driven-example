using Microsoft.Extensions.Hosting;

namespace Example.EventDriven.Domain.Gateways.Event
{
    public abstract class BaseWorker<TEvent> : BackgroundService
    {
        protected abstract Task ProcessEventAsync(TEvent request, CancellationToken cancellationToken);
    }
}
