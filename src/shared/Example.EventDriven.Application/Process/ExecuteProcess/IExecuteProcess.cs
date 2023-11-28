using Example.EventDriven.Application.ExecuteProcess.Boundaries;

namespace Example.EventDriven.Application.ExecuteProcess
{
    public interface IExecuteProcess
    {
        Task<ExecuteProcessResponse> Execute(ExecuteProcessRequest request, CancellationToken cancellationToken);
    }
}
