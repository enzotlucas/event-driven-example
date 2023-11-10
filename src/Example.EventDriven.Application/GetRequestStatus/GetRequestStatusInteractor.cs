using Example.EventDriven.Application.GetRequestStatus.Boundaries;

namespace Example.EventDriven.Application.GetRequestStatus
{
    public class GetRequestStatusInteractor : IGetRequestStatus
    {
        public Task<GetRequestStatusResponse> Get(GetRequestStatusRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
