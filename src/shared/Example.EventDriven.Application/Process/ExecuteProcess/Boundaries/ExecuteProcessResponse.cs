using Example.EventDriven.Domain.Entitites;

namespace Example.EventDriven.Application.ExecuteProcess.Boundaries
{
    public sealed class ExecuteProcessResponse
    {
        public RequestEntity<ProcessEntity> Value { get; set; }
    }
}
