namespace Example.EventDriven.Application.CreateProcess.Boundaries
{
    public sealed class CreateProcessRequest
    {
        public Guid RequestId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CreateProcessRequest(){ }

        public CreateProcessRequest(Guid requestId, string name, string description)
        {
            RequestId = requestId;
            Name = name;
            Description = description;
        }
    }
}
