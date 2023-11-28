using Example.EventDriven.Application.Request.UpdateRequest.Boundaries;
using Example.EventDriven.Domain.Gateways.Event;
using Example.EventDriven.Domain.Gateways.Logger;
using Example.EventDriven.Domain.Gateways.MemoryCache;
using FluentValidation;

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

        public Task Update(UpdateRequestStatusRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
