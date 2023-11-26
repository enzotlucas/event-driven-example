using Example.EventDriven.Application.ExecuteProcess.Boundaries;

namespace Example.EventDriven.Application.ExecuteProcess
{
    public sealed class ExecuteProcessInteractor : IExecuteProcess
    {
        public ExecuteProcessInteractor()
        {
            
        }

        public Task<ExecuteProcessResponse> Execute(ExecuteProcessRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
