using Example.EventDriven.Application.CreateProccess.Boundaries;
using Example.EventDriven.Domain.Gateways.Event;
using Example.EventDriven.Domain.Gateways.Logger;
using Example.EventDriven.Domain.Gateways.MemoryCache;
using Example.EventDriven.Domain.Extensions;
using FluentValidation;
using Example.EventDriven.Domain.Entitites;
using Mapster;
using Example.EventDriven.Domain.Gateways.Event.Events;

namespace Example.EventDriven.Application.CreateProccess
{
    public class CreateProcessInteractor : ICreateProcess
    {
        private readonly ILoggerManager _logger;
        private readonly IEventManager _eventManager;
        private readonly IValidator<CreateProccessRequest> _validator;

        public CreateProcessInteractor(ILoggerManager logger, IEventManager eventManager, IValidator<CreateProccessRequest> validator)
        {
            _logger = logger;
            _eventManager = eventManager;
            _validator = validator;
        }

        public async Task<CreateProccessResponse> Create(CreateProccessRequest request, CancellationToken cancellationToken)
        {
            _logger.Log("Starting create process", LoggerManagerSeverity.INFORMATION, ("request", request));

            _logger.Log("Validating the request", LoggerManagerSeverity.DEBUG, ("request", request));

            _validator.ThrowIfInvalid(request);

            _logger.Log("Request is valid", LoggerManagerSeverity.DEBUG, ("request", request));

            var process = request.Adapt<ProcessEntity>();

            _logger.Log("Sending process event", LoggerManagerSeverity.DEBUG, ("process", process));

            var requestId = await _eventManager.SendEvent(process.Adapt<CreateProcessEvent>(), cancellationToken);

            _logger.Log("Create process event sent", LoggerManagerSeverity.DEBUG, ("process", process), ("requestId", requestId));

            var response = new CreateProccessResponse(requestId);

            _logger.Log("Ending create process", LoggerManagerSeverity.INFORMATION, ("response", response));

            return response;
        }
    }
}
