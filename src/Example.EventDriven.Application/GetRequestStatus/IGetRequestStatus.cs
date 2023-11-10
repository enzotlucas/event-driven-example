using Example.EventDriven.Application.GetRequestStatus.Boundaries;

namespace Example.EventDriven.Application.GetRequestStatus
{
    public interface IGetRequestStatus
    {
        Task<GetRequestStatusResponse> Get(GetRequestStatusRequest request, CancellationToken cancellationToken);
    }
}
