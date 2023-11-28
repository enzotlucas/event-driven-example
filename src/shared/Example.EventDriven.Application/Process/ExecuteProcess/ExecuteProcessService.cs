using Example.EventDriven.Application.ExecuteProcess.Boundaries;
using Example.EventDriven.Domain.Gateways.Event;
using Microsoft.Extensions.DependencyInjection;
using Example.EventDriven.Application.ExecuteProcess;
using Example.EventDriven.Domain.Gateways.Logger;
using Example.EventDriven.Domain.Gateways.MemoryCache;
using Example.EventDriven.Application.Request.UpdateRequest.Boundaries;
using Mapster;

namespace Example.EventDriven.Process.ExecuteProcess.Application
{
    public sealed class ExecuteProcessService : BaseWorker<ExecuteProcessEvent>
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

            var eventSender = scope.ServiceProvider.GetRequiredService<IEventSenderManager>();
            var executeProcessService = scope.ServiceProvider.GetRequiredService<IExecuteProcess>();

            _logger.Log("Begin executing Execute Process business rule", LoggerManagerSeverity.DEBUG, ("request", request));
            var response = await executeProcessService.Execute(request.Value, cancellationToken);
            response.RequestId = request.RequestId;
            _logger.Log("End executing Execute Process business rule", LoggerManagerSeverity.DEBUG,
                ("request", request), ("response", response));

            _logger.Log("Begin saving Execute Process business rule response on memory cache", LoggerManagerSeverity.DEBUG);
            await eventSender.Send(response.Adapt<UpdateRequestStatusEvent>(), cancellationToken);
            //await memoryCache.CreateOrUpdate(request.RequestId, response.Value);
            _logger.Log("End saving Execute Process business rule response on memory cache", LoggerManagerSeverity.DEBUG);
        }
    }
}
