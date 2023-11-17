using Microsoft.AspNetCore.Mvc;

namespace Example.EventDriven.API.Features.Core
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        private readonly int _cancelRequisitionAfterInSeconds;

        public BaseController(IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            _cancelRequisitionAfterInSeconds = configuration.GetValue<int>("CancelRequisitionAfterInSeconds");
        }

        protected CancellationToken AsCombinedCancellationToken(CancellationToken requestCancellationToken)
        {
            using var combinedCancellationTokens = CancellationTokenSource.CreateLinkedTokenSource(requestCancellationToken, HttpContext.RequestAborted);

            combinedCancellationTokens.CancelAfter(_cancelRequisitionAfterInSeconds);

            return combinedCancellationTokens.Token;
        }
    }
}
