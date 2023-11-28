using Example.EventDriven.Domain.Entitites;

namespace Example.EventDriven.Application.Request.UpdateRequest.Boundaries
{
    public sealed record UpdateRequestStatusRequest(RequestEntity<ProcessEntity> Value, Guid RequestId);
}
