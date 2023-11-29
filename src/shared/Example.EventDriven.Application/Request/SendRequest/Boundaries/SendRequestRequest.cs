using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Application.SendEvent.Boundaries
{
    [ExcludeFromCodeCoverage]
    public sealed record SendEventRequest(string OperationName, object Value);
}
