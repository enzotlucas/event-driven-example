using Example.EventDriven.API.Features.Core;
using Example.EventDriven.Application.CreateProcess.Boundaries;
using Example.EventDriven.Application.GetRequestStatus;
using Example.EventDriven.Application.GetRequestStatus.Boundaries;
using Example.EventDriven.Application.SendEvent;
using Example.EventDriven.Application.SendEvent.Boundaries;
using Example.EventDriven.Domain.Entitites;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Example.EventDriven.API.Features.V1.Controllers
{
    [ApiVersion("1.0")]
    public sealed class ProccessController : BaseController
    {
        private readonly ISendEvent _sendEvent;
        private readonly IGetRequestStatus _getRequestStatus;

        public ProccessController(IConfiguration configuration, ISendEvent sendEvent, IGetRequestStatus getRequestStatus) : base(configuration)
        {
            _sendEvent = sendEvent;
            _getRequestStatus = getRequestStatus;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProcessRequest request, CancellationToken cancellationToken)
        {
            var response = await _sendEvent.Send(request.Adapt<SendEventRequest>(), cancellationToken);

            return Accepted(response);
        }

        [HttpGet("status/{id:guid}")]
        public async Task<IActionResult> GetRequestStatus([FromRoute] GetRequestStatusRequest request, CancellationToken cancellationToken)
        {
            var response = await _getRequestStatus.Get<ProcessEntity>(request, cancellationToken);

            return StatusCode(response.StatusCode, response.Data);
        }
    }
}
