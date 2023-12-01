using Example.EventDriven.Application.ExecuteProcess.Boundaries;
using Example.EventDriven.Domain.ValueObjects;
using FluentValidation;

namespace Example.EventDriven.Application.ExecuteProcess
{
    public sealed class ExecuteProcessValidator : AbstractValidator<ExecuteProcessRequest>
    {
        public ExecuteProcessValidator()
        {
            RuleFor(request => request.Name)
              .NotEmpty()
              .WithMessage(ResponseMessage.InvalidName.ToString());
        }
    }
}
