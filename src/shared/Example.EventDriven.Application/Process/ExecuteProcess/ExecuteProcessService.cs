using Example.EventDriven.Application.ExecuteProcess.Boundaries;
using Example.EventDriven.Domain.Gateways.Event;
using Microsoft.Extensions.DependencyInjection;
using Example.EventDriven.Application.ExecuteProcess;
using Example.EventDriven.Domain.Gateways.Logger;
using Example.EventDriven.Domain.Gateways.MemoryCache;

namespace Example.EventDriven.Process.ExecuteProcess.Application
{
    public class ExecuteProcessService : BaseWorker<ExecuteProcessEvent>
    {
        private readonly IEventConsumerManager _eventConsumer;
        private readonly ILoggerManager _logger;
        private readonly IServiceProvider _serviceProvider;

        public ExecuteProcessService(IEventConsumerManager eventConsumer,
                                     ILoggerManager logger,
                                     IServiceProvider serviceProvider)
        {
            _eventConsumer = eventConsumer;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) =>
            await _eventConsumer.SubscribeAsync<ExecuteProcessEvent, ExecuteProcessRequest>(
                nameof(ExecuteProcessEvent),
                async (request, requestCancellationToken) => await ProcessEventAsync(request, requestCancellationToken),
                configuration => configuration.WithTopic(nameof(ExecuteProcessEvent)),
                stoppingToken);

        protected override async Task ProcessEventAsync(ExecuteProcessEvent request, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var memoryCache = scope.ServiceProvider.GetRequiredService<IMemoryCacheManager>();
            var executeProcessService = scope.ServiceProvider.GetRequiredService<IExecuteProcess>();

            _logger.Log("Begin executing Execute Process business rule", LoggerManagerSeverity.DEBUG, ("request", request));
            var response = await executeProcessService.Execute(request.Value, cancellationToken);
            _logger.Log("End executing Execute Process business rule", LoggerManagerSeverity.DEBUG,
                ("request", request), ("response", response));

            _logger.Log("Begin saving Execute Process business rule response on memory cache", LoggerManagerSeverity.DEBUG);
            await memoryCache.CreateOrUpdate(request.RequestId, response.Value);
            _logger.Log("End saving Execute Process business rule response on memory cache", LoggerManagerSeverity.DEBUG);
        }
    }
}
