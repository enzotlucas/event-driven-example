using Example.EventDriven.Application.CreateProccess;
using Example.EventDriven.Application.CreateProccess.Boundaries;
using Example.EventDriven.Application.GetRequestStatus;
using Example.EventDriven.Application.GetRequestStatus.Boundaries;
using Example.EventDriven.Domain.Entitites;
using Microsoft.AspNetCore.Mvc;

namespace Example.EventDriven.API.Features.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class ProccessController : ControllerBase
    {
        private readonly ICreateProcess _createProcess;
        private readonly IGetRequestStatus _getRequestStatus;

        public ProccessController(ICreateProcess createProcess, IGetRequestStatus getRequestStatus)
        {
            _createProcess = createProcess;
            _getRequestStatus = getRequestStatus;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProccessRequest request, CancellationToken cancellationToken)
        {
            var response = await _createProcess.Create(request, cancellationToken);

            return CreatedAtAction(nameof(Post), response);
        }

        [HttpGet("status/{id:guid}")]
        public async Task<IActionResult> GetRequestStatus([FromRoute] GetRequestStatusRequest request, CancellationToken cancellationToken)
        {
            var response = await _getRequestStatus.Get<ProcessEntity>(request, cancellationToken);

            return StatusCode(response.StatusCode, response.Data);
        }
    }
}
