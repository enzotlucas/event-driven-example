using Example.EventDriven.Domain.Entitites;
using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Application.ExecuteProcess.Boundaries
{
    [ExcludeFromCodeCoverage]
    public sealed class ExecuteProcessResponse
    {
        public RequestEntity<ProcessEntity> Value { get; set; }
        public Guid RequestId { get; set; }
    }
}
