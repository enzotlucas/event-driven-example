using Example.EventDriven.Application.CreateProccess;
using Example.EventDriven.Application.CreateProccess.Boundaries;
using Example.EventDriven.Domain.Gateways.Event;
using Example.EventDriven.Domain.Gateways.MemoryCache;
using Microsoft.Extensions.DependencyInjection;

namespace Example.EventDriven.CreateProcess.Worker
{
    public sealed class CreateProcessService : BaseWorker<CreateProcessEvent>
    {
        private readonly IEventConsumerManager _eventConsumer;
        private readonly IServiceProvider _serviceProvider;

        public CreateProcessService(IEventConsumerManager eventConsumer,
                                    IServiceProvider serviceProvider)
        {
            _eventConsumer = eventConsumer;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) => 
            await _eventConsumer.SubscribeAsync<CreateProcessEvent, CreateProcessRequest>(
                nameof(CreateProcessEvent),
                async (request, requestCancellationToken) => await ProcessEventAsync(request, requestCancellationToken),
                configuration => configuration.WithTopic(nameof(CreateProcessEvent)),
                stoppingToken);

        protected override async Task ProcessEventAsync(CreateProcessEvent request, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var memoryCache = scope.ServiceProvider.GetRequiredService<IMemoryCacheManager>();

            var createProcessService = scope.ServiceProvider.GetRequiredService<ICreateProcess>();

            await createProcessService.Create(request.Value, cancellationToken);
        }
    }
}
