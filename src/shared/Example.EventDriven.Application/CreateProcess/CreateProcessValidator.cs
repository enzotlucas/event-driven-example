using Example.EventDriven.Application.CreateProccess.Boundaries;
using FluentValidation;

namespace Example.EventDriven.Application.CreateProccess
{
    public class CreateProcessValidator : AbstractValidator<CreateProcessRequest>
    {
        public CreateProcessValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty();

            RuleFor(request => request.Description)
                .NotEmpty();
        }
    }
}
