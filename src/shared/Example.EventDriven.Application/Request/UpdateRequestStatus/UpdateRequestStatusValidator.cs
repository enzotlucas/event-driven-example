using Example.EventDriven.Application.Request.UpdateRequest.Boundaries;
using FluentValidation;

namespace Example.EventDriven.Application.Request.UpdateRequest
{
    public sealed class UpdateRequestStatusValidator : AbstractValidator<UpdateRequestStatusRequest>
    {
        public UpdateRequestStatusValidator()
        {
            
        }
    }
}
