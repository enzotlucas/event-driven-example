using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Application.Request.SendRequest.Boundaries
{
    [ExcludeFromCodeCoverage]
    public sealed record SendEventRequest(string OperationName, object Value);
}
