using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Application.Request.SendRequest.Boundaries
{
    [ExcludeFromCodeCoverage]
    public sealed record SendRequestResponse(Guid RequestId);
}
