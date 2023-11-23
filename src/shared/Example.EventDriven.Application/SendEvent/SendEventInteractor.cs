using Example.EventDriven.Application.CreateProccess.Boundaries;
using Example.EventDriven.Application.SendEvent.Boundaries;
using Example.EventDriven.Domain.Extensions;
using Example.EventDriven.Domain.Gateways.Event;
using Example.EventDriven.Domain.Gateways.Logger;
using FluentValidation;
using Mapster;

namespace Example.EventDriven.Application.SendEvent
{
    public class SendEventInteractor : ISendEvent
    {
        private readonly ILoggerManager _logger;
        private readonly IEventSenderManager _eventManager;
        private readonly IValidator<SendEventRequest> _validator;

        public SendEventInteractor(ILoggerManager logger, IEventSenderManager eventManager, IValidator<SendEventRequest> validator)
        {
            _logger = logger;
            _eventManager = eventManager;
            _validator = validator;
        }

        public async Task<SendEventResponse> Send(SendEventRequest request, CancellationToken cancellationToken)
        {
            _logger.Log("Starting sending the event", LoggerManagerSeverity.INFORMATION, ("request", request));

            _logger.Log("Validating the request", LoggerManagerSeverity.DEBUG, ("request", request));
            _validator.ThrowIfInvalid(request);
            _logger.Log("Request is valid", LoggerManagerSeverity.DEBUG, ("request", request));

            _logger.Log("Sending event", LoggerManagerSeverity.DEBUG, ("request", request));
            var requestId = await _eventManager.Send(request.Adapt<CreateProcessEvent>(), cancellationToken);
            _logger.Log("Event sent", LoggerManagerSeverity.DEBUG, ("request", request), ("requestId", requestId));

            var response = new SendEventResponse(requestId);

            _logger.Log("Ending sending the event", LoggerManagerSeverity.INFORMATION, ("response", response));

            return response;
        }
    }
}
