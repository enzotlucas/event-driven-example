namespace Example.EventDriven.Domain.ValueObjects
{
    public enum ResponseMessage
    {
        Default,
        ProcessAlreadyExists,
        ErrorCreatingProcess,
        ProcessDontExists,
        InvalidName,
        InvalidDescription
    }
}
