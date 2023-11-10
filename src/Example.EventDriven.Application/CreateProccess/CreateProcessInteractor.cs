using Example.EventDriven.Application.CreateProccess.Boundaries;

namespace Example.EventDriven.Application.CreateProccess
{
    public class CreateProcessInteractor : ICreateProcess
    {
        public Task<CreateProccessResponse> Create(CreateProccessRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
