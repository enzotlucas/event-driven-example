namespace Example.EventDriven.Application.SendEvent.Boundaries
{
    public sealed record SendEventRequest(string OperationName, object Value);
}
