using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Domain.Gateways.Event
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseWorker<TEvent> : BackgroundService
    {
        protected abstract Task ProcessEventAsync(TEvent request, CancellationToken cancellationToken);
    }
}
