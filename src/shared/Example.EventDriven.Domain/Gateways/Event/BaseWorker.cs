using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Domain.Gateways.Event
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseWorker<TEvent> : BackgroundService
    {
        protected abstract Task ProcessEventAsync(TEvent request, CancellationToken cancellationToken);
        protected abstract void Subscribe(CancellationToken cancellationToken);
        protected void RefreshConnection(object s, EventArgs e)
        {
            Subscribe(CancellationToken.None);
        }
    }
}
