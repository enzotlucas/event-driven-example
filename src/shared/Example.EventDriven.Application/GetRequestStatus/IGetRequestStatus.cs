using Example.EventDriven.Application.GetRequestStatus.Boundaries;

namespace Example.EventDriven.Application.GetRequestStatus
{
    public interface IGetRequestStatus
    {
        Task<GetRequestStatusResponse<T>> Get<T>(GetRequestStatusRequest request, CancellationToken cancellationToken);
    }
}
