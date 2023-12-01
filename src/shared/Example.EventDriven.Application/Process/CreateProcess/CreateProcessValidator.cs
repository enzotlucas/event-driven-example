using Example.EventDriven.Application.CreateProcess.Boundaries;
using Example.EventDriven.Domain.ValueObjects;
using FluentValidation;

namespace Example.EventDriven.Application.CreateProcess
{
    public sealed class CreateProcessValidator : AbstractValidator<CreateProcessRequest>
    {
        public CreateProcessValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage(ResponseMessage.InvalidName.ToString());

            RuleFor(request => request.Description)
                .NotEmpty()
                .WithMessage(ResponseMessage.InvalidDescription.ToString());
        }
    }
}
