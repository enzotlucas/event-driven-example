using Example.EventDriven.Application.CreateProcess.Boundaries;
using Example.EventDriven.Application.Request.UpdateRequest.Boundaries;
using Example.EventDriven.Application.SendEvent.Boundaries;
using Example.EventDriven.Domain.Entitites;
using Example.EventDriven.Domain.Extensions;
using Example.EventDriven.Domain.Gateways.Event;
using Example.EventDriven.Domain.Gateways.Logger;
using Example.EventDriven.Domain.Gateways.MemoryCache;
using FluentValidation;
using Mapster;

namespace Example.EventDriven.Application.Request.UpdateRequest
{
    public sealed class UpdateRequestStatusInteractor : IUpdateRequestStatus
    {
        private readonly ILoggerManager _logger;
        private readonly IEventSenderManager _eventManager;
        private readonly IValidator<UpdateRequestStatusRequest> _validator;
        private readonly IMemoryCacheManager _memoryCache;


        public UpdateRequestStatusInteractor(
            ILoggerManager logger, 
            IEventSenderManager eventManager, 
            IValidator<UpdateRequestStatusRequest> validator, 
            IMemoryCacheManager memoryCache)
        {
            _logger = logger;
            _eventManager = eventManager;
            _validator = validator;
            _memoryCache = memoryCache;
        }

        public async Task Update(UpdateRequestStatusRequest request, CancellationToken cancellationToken)
        {
            _logger.Log("Starting updating the request status", LoggerManagerSeverity.INFORMATION, ("request", request));
            _logger.Log("Validating the request", LoggerManagerSeverity.DEBUG, ("request", request));

            var validation = await _validator.ValidateAsync(request, cancellationToken);

            if (!validation.IsValid)
            {
                _logger.Log("Request is not valid", LoggerManagerSeverity.WARNING,
                        ("request", request),
                        ("validation", validation));

                return;
            }

            _logger.Log("Request is valid", LoggerManagerSeverity.DEBUG, ("request", request));

            if (!await _memoryCache.ExistsAsync(request.RequestId, cancellationToken))
            {
                _logger.Log("Request id does not exists", LoggerManagerSeverity.WARNING,
                        ("request", request),
                        ("validation", validation));

                return;
            }

            _logger.Log("Updating request on memory cache", LoggerManagerSeverity.DEBUG, ("requestEntity", request.Value), ("requestId", request.RequestId));
            await _memoryCache.CreateOrUpdate(request.RequestId, request.Value, cancellationToken);
            _logger.Log("Request updated on memory cache", LoggerManagerSeverity.DEBUG, ("requestEntity", request.Value), ("requestId", request.RequestId));

            _logger.Log("Ending sending the event", LoggerManagerSeverity.INFORMATION, ("request", request));
        }
    }
}
