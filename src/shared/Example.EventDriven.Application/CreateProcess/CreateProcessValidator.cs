using Example.EventDriven.Application.CreateProcess.Boundaries;
using FluentValidation;

namespace Example.EventDriven.Application.CreateProcess
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
