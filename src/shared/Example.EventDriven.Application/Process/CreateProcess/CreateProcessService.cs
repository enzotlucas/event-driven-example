using Example.EventDriven.Application.CreateProcess;
using Example.EventDriven.Application.CreateProcess.Boundaries;
using Example.EventDriven.Domain.Gateways.Event;
using Example.EventDriven.Domain.Gateways.Logger;
using Example.EventDriven.Domain.Gateways.MemoryCache;
using Microsoft.Extensions.DependencyInjection;

namespace Example.EventDriven.Application.Process.CreateProcess
{
    public sealed class CreateProcessService : BaseWorker<CreateProcessEvent>
    {
        private readonly IEventConsumerManager _eventConsumer;
        private readonly ILoggerManager _logger;
        private readonly IServiceProvider _serviceProvider;

        public CreateProcessService(
            IEventConsumerManager eventConsumer,
            ILoggerManager logger,
            IServiceProvider serviceProvider)
        {
            _eventConsumer = eventConsumer;
            _logger = logger;
            _serviceProvider = serviceProvider;

            _logger.Log("Application started", LoggerManagerSeverity.INFORMATION);
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

            _logger.Log("Begin executing Create Process business rule", LoggerManagerSeverity.DEBUG, ("request", request));
            var response = await createProcessService.Create(request.Value, cancellationToken);
            _logger.Log("End executing Create Process business rule", LoggerManagerSeverity.DEBUG, 
                    ("request", request), 
                    ("response", response));

            _logger.Log("Begin saving Create Process business rule response on memory cache", LoggerManagerSeverity.DEBUG);
            await memoryCache.CreateOrUpdate(request.RequestId, response.Value);
            _logger.Log("End saving Create Process business rule response on memory cache", LoggerManagerSeverity.DEBUG);
        }
    }
}
