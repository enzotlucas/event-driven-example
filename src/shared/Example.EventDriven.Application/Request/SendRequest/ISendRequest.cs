using Example.EventDriven.Application.Request.SendRequest.Boundaries;

namespace Example.EventDriven.Application.Request.SendRequest
{
    public interface ISendRequest
    {
        Task<SendRequestResponse> Send(SendEventRequest request, CancellationToken cancellationToken);
    }
}
