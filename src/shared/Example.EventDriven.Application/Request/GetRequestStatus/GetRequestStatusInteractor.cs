using Example.EventDriven.Application.GetRequestStatus.Boundaries;
using Example.EventDriven.Domain.Gateways.Logger;
using Example.EventDriven.Domain.Gateways.MemoryCache;
using Example.EventDriven.Domain.Extensions;
using FluentValidation;
using Example.EventDriven.Domain.Entitites;

namespace Example.EventDriven.Application.GetRequestStatus
{
    public sealed class GetRequestStatusInteractor : IGetRequestStatus
    {
        private readonly ILoggerManager _logger;
        private readonly IMemoryCacheManager _memoryCache;
        private readonly IValidator<GetRequestStatusRequest> _validator;

        public GetRequestStatusInteractor(ILoggerManager logger, IMemoryCacheManager memoryCache, IValidator<GetRequestStatusRequest> validator)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _validator = validator;
        }

        public async Task<GetRequestStatusResponse<T>> Get<T>(GetRequestStatusRequest request, CancellationToken cancellationToken)
        {
            _logger.Log("Starting get request status", LoggerManagerSeverity.INFORMATION, ("request", request));

            _logger.Log("Validating the request", LoggerManagerSeverity.DEBUG, ("request", request));
            _validator.ThrowIfInvalid(request);
            _logger.Log("Request is valid", LoggerManagerSeverity.DEBUG, ("request", request));

            var value = await _memoryCache.GetAsync<RequestEntity<T>>(request.RequestId, cancellationToken);

            _logger.Log("Ending get request status", LoggerManagerSeverity.INFORMATION, ("response", value));

            return new GetRequestStatusResponse<T>(value);
        }
    }
}
