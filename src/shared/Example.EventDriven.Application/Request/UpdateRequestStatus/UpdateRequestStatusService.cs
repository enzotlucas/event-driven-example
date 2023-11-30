using Example.EventDriven.Application.ExecuteProcess.Boundaries;
using Example.EventDriven.Application.Request.UpdateRequest.Boundaries;
using Example.EventDriven.Domain.Gateways.Event;
using Example.EventDriven.Domain.Gateways.Logger;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace Example.EventDriven.Application.Request.UpdateRequest
{
    public class UpdateRequestStatusService : BaseWorker<UpdateRequestStatusEvent>
    {
        private readonly IEventConsumerManager _eventConsumer;
        private readonly ILoggerManager _logger;
        private readonly IServiceProvider _serviceProvider;

        public UpdateRequestStatusService(IEventConsumerManager eventConsumer,
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
            _eventConsumer.SubscribeAsync<UpdateRequestStatusEvent, UpdateRequestStatusRequest>(
                nameof(UpdateRequestStatusEvent),
                async (request, requestCancellationToken) => await ProcessEventAsync(request, requestCancellationToken),
                configuration => configuration.WithTopic(nameof(UpdateRequestStatusEvent)),
                cancellationToken);

            _eventConsumer.MessageBus.Advanced.Connected += RefreshConnection;

            _logger.Log("Update request status Service is listening", LoggerManagerSeverity.INFORMATION);
        }

        protected override async Task ProcessEventAsync(UpdateRequestStatusEvent request, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var updateRequestStatusService = scope.ServiceProvider.GetRequiredService<IUpdateRequestStatus>();

            _logger.Log("Begin executing Update Request Status business rule", LoggerManagerSeverity.DEBUG, ("request", request));

            await updateRequestStatusService.Update(request.Value, cancellationToken);

            _logger.Log("End executing Update Request Status business rule", LoggerManagerSeverity.DEBUG, ("request", request));
        }
    }
}