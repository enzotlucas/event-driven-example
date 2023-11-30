using Example.EventDriven.Application.CreateProcess;
using Example.EventDriven.Application.CreateProcess.Boundaries;
using Example.EventDriven.Application.Request.UpdateRequest.Boundaries;
using Example.EventDriven.Domain.Gateways.Event;
using Example.EventDriven.Domain.Gateways.Logger;
using Mapster;
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
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Subscribe(stoppingToken);

            return Task.CompletedTask;
        }

        protected override void Subscribe(CancellationToken cancellationToken)
        {
            _eventConsumer.SubscribeAsync<CreateProcessEvent, CreateProcessRequest>(
                nameof(CreateProcessEvent),
                async (request, requestCancellationToken) => await ProcessEventAsync(request, requestCancellationToken),
                configuration => configuration.WithTopic(nameof(CreateProcessEvent)),
                cancellationToken);

            _eventConsumer.MessageBus.Advanced.Connected += RefreshConnection;

            _logger.Log("Create Process Service is listening", LoggerManagerSeverity.INFORMATION);
        }

        protected override async Task ProcessEventAsync(CreateProcessEvent request, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var eventSender = scope.ServiceProvider.GetRequiredService<IEventSenderManager>();
            var createProcessService = scope.ServiceProvider.GetRequiredService<ICreateProcess>();

            _logger.Log("Begin executing Create Process business rule", LoggerManagerSeverity.DEBUG, ("request", request));
            var response = await createProcessService.Create(request.Value, cancellationToken);
            response.RequestId = request.RequestId;
            _logger.Log("End executing Create Process business rule", LoggerManagerSeverity.DEBUG, 
                    ("request", request), 
                    ("response", response));

            _logger.Log("Begin saving Create Process business rule response on memory cache", LoggerManagerSeverity.DEBUG);
            await eventSender.Send<UpdateRequestStatusEvent, UpdateRequestStatusRequest>(response.Adapt<UpdateRequestStatusEvent>(), cancellationToken);
            _logger.Log("End saving Create Process business rule response on memory cache", LoggerManagerSeverity.DEBUG);
        }
    }
}
