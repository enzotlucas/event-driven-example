using Example.EventDriven.Application.CreateProccess.Boundaries;

namespace Example.EventDriven.Application.CreateProccess
{
    public class CreateProcessInteractor : ICreateProcess
    {
        public async Task<CreateProcessResponse> Create(CreateProcessRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
