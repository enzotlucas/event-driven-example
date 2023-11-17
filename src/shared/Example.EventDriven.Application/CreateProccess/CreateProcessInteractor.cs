using Example.EventDriven.Application.CreateProccess.Boundaries;

namespace Example.EventDriven.Application.CreateProccess
{
    public class CreateProcessInteractor : ICreateProcess
    {
        public async Task<CreateProccessResponse> Create(CreateProccessRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
