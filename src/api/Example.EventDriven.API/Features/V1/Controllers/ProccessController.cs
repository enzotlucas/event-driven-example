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

        /// <summary>
        /// Creates a process and starts its execution
        /// </summary>
        /// <param name="request">The request body</param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">The created request id</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted, StatusCode = StatusCodes.Status202Accepted, Type = typeof(CreateProcessResponse))]
        public async Task<IActionResult> Post(CreateProcessRequest request, CancellationToken cancellationToken)
        {
            var response = await _sendEvent.Send(request.Adapt<SendEventRequest>(), cancellationToken);

            return Accepted(response);
        }

        /// <summary>
        /// Gets a current request status
        /// </summary>
        /// <param name="request">The request id</param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">The request status</response>
        /// <response code="204">The request execution was't started</response>
        /// <response code="404">The request don't exists</response>
        /// <response code="422">The request body was not valid</response>
        /// <response code="500">The request has a unexpected error</response>
        [HttpGet("status/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, StatusCode = StatusCodes.Status200OK, Type = typeof(GetRequestStatusResponse<ProcessEntity>))]
        [ProducesResponseType(StatusCodes.Status204NoContent, StatusCode = StatusCodes.Status204NoContent, Type = typeof(GetRequestStatusResponse<ProcessEntity>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, StatusCode = StatusCodes.Status404NotFound, Type = typeof(GetRequestStatusResponse<ProcessEntity>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, StatusCode = StatusCodes.Status422UnprocessableEntity, Type = typeof(GetRequestStatusResponse<ProcessEntity>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, StatusCode = StatusCodes.Status500InternalServerError, Type = typeof(GetRequestStatusResponse<ProcessEntity>))]
        public async Task<IActionResult> GetRequestStatus([FromRoute] GetRequestStatusRequest request, CancellationToken cancellationToken)
        {
            var response = await _getRequestStatus.Get<ProcessEntity>(request, cancellationToken);

            return StatusCode(response.StatusCode, response.Data);
        }
    }
}
