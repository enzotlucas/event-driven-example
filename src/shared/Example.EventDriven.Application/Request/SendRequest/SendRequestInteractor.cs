using Example.EventDriven.Application.CreateProcess.Boundaries;
using Example.EventDriven.Application.SendEvent.Boundaries;
using Example.EventDriven.Domain.Entitites;
using Example.EventDriven.Domain.Extensions;
using Example.EventDriven.Domain.Gateways.Event;
using Example.EventDriven.Domain.Gateways.Logger;
using Example.EventDriven.Domain.Gateways.MemoryCache;
using FluentValidation;
using Mapster;

namespace Example.EventDriven.Application.SendEvent
{
    public sealed class SendRequestInteractor : ISendRequest
    {
        private readonly ILoggerManager _logger;
        private readonly IEventSenderManager _eventManager;
        private readonly IValidator<SendEventRequest> _validator;
        private readonly IMemoryCacheManager _memoryCache;

        public SendRequestInteractor(
            ILoggerManager logger, 
            IEventSenderManager eventManager, 
            IValidator<SendEventRequest> validator, 
            IMemoryCacheManager memoryCache)
        {
            _logger = logger;
            _eventManager = eventManager;
            _validator = validator;
            _memoryCache = memoryCache;
        }

        public async Task<SendRequestResponse> Send(SendEventRequest request, CancellationToken cancellationToken)
        {
            _logger.Log("Starting sending the event", LoggerManagerSeverity.INFORMATION, ("request", request));

            _logger.Log("Validating the request", LoggerManagerSeverity.DEBUG, ("request", request));
            _validator.ThrowIfInvalid(request);
            _logger.Log("Request is valid", LoggerManagerSeverity.DEBUG, ("request", request));

            _logger.Log("Sending event", LoggerManagerSeverity.DEBUG, ("request", request));
            var requestId = await _eventManager.Send(request.Adapt<CreateProcessEvent>(), cancellationToken);
            _logger.Log("Event sent", LoggerManagerSeverity.DEBUG, ("request", request), ("requestId", requestId));

            var requestEntity = new RequestEntity<ProcessEntity>
            {
                RequestId = requestId,
                Status = Domain.ValueObjects.RequestStatus.NotStarted,
                Message = Domain.ValueObjects.ResponseMessage.Default
            };

            _logger.Log("Creating request on memory cache", LoggerManagerSeverity.DEBUG, ("requestEntity", requestEntity), ("requestId", requestId));
            await _memoryCache.CreateOrUpdate(requestId, requestEntity);
            _logger.Log("Request created on memory cache", LoggerManagerSeverity.DEBUG, ("requestEntity", requestEntity), ("requestId", requestId));

            _logger.Log("Ending sending the event", LoggerManagerSeverity.INFORMATION, ("requestEntity", requestEntity));

            return new SendRequestResponse(requestId);
        }
    }
}
