using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Example.EventDriven.Application.CreateProcess.Boundaries
{
    [ExcludeFromCodeCoverage]
    public sealed class CreateProcessRequest
    {
        [JsonIgnore]
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
