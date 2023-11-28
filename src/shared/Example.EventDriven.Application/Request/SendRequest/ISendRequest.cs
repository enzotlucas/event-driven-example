using Example.EventDriven.Application.SendEvent.Boundaries;

namespace Example.EventDriven.Application.SendEvent
{
    public interface ISendRequest
    {
        Task<SendRequestResponse> Send(SendEventRequest request, CancellationToken cancellationToken);
    }
}
