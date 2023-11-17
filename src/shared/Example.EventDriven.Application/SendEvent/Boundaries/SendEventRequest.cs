namespace Example.EventDriven.Application.SendEvent.Boundaries
{
    public record SendEventRequest(string OperationName, object Value);
}
