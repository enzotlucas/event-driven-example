using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Application.GetRequestStatus.Boundaries
{
    [ExcludeFromCodeCoverage]
    public sealed record GetRequestStatusRequest(Guid RequestId);
}
