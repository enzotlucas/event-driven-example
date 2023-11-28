using Example.EventDriven.Application.Request.UpdateRequest.Boundaries;

namespace Example.EventDriven.Application.Request.UpdateRequest
{
    public interface IUpdateRequestStatus
    {
        Task<UpdateRequestStatusResponse> Update(UpdateRequestStatusRequest request, CancellationToken cancellationToken);
    }
}
