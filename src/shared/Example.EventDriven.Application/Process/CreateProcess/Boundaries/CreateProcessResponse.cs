using Example.EventDriven.Domain.Entitites;
using Example.EventDriven.Domain.ValueObjects;

namespace Example.EventDriven.Application.CreateProcess.Boundaries
{
    public sealed class CreateProcessResponse
    {
        public RequestEntity<ProcessEntity> Value { get; set; }
    }
}
