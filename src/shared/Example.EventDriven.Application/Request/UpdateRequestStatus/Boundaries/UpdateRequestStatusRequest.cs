using Example.EventDriven.Domain.Entitites;
using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Application.Request.UpdateRequest.Boundaries
{
    [ExcludeFromCodeCoverage]
    public sealed record UpdateRequestStatusRequest(RequestEntity<ProcessEntity> Value, Guid RequestId);
}
