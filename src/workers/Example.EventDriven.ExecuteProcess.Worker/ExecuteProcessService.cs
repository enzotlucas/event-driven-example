using Example.EventDriven.Application.ExecuteProcess.Boundaries;
using Example.EventDriven.Domain.Gateways.Event;
using Microsoft.Extensions.DependencyInjection;
using Example.EventDriven.Application.ExecuteProcess;

namespace Example.EventDriven.ExecuteProcess.Worker
{
    public class ExecuteProcessService : BaseWorker<ExecuteProcessEvent>
    {
        private readonly IEventConsumerManager _eventConsumer;
        private readonly IServiceProvider _serviceProvider;

        public ExecuteProcessService(IEventConsumerManager eventConsumer,
                                     IServiceProvider serviceProvider)
        {
            _eventConsumer = eventConsumer;
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

            var executeProcessService = scope.ServiceProvider.GetRequiredService<IExecuteProcess>();

            await executeProcessService.Execute(request.Value, cancellationToken);
        }
    }
}
