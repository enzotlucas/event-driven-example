using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Application.ExecuteProcess.Boundaries
{
    [ExcludeFromCodeCoverage]
    public sealed class ExecuteProcessRequest
    {
        public string Name { get; set; }
    }
}
