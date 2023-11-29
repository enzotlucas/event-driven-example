using Example.EventDriven.Domain.Entitites;
using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Application.CreateProcess.Boundaries
{
    [ExcludeFromCodeCoverage]
    public sealed class CreateProcessResponse
    {
        public RequestEntity<ProcessEntity> Value { get; set; }
        public Guid RequestId { get; set; }
    }
}
