using Example.EventDriven.Application.SendEvent.Boundaries;

namespace Example.EventDriven.Application.SendEvent
{
    public interface ISendEvent
    {
        Task<SendEventResponse> Send(SendEventRequest request, CancellationToken cancellationToken);
    }
}
