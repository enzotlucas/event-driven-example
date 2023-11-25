namespace Example.EventDriven.Domain.ValueObjects
{
    public enum RequestStatus
    {
        NotStarted,
        Processing,
        Completed,
        Canceled,
        InvalidInformation,
        InfrastructureError
    }
}
