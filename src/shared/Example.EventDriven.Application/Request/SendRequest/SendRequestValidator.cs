using Example.EventDriven.Application.SendEvent.Boundaries;
using FluentValidation;

namespace Example.EventDriven.Application.SendEvent
{
    public sealed class SendRequestValidator : AbstractValidator<SendEventRequest>
    {
        public SendRequestValidator()
        {
            RuleFor(request => request.OperationName).NotEmpty();

            RuleFor(request => request.Value).NotNull();
        }
    }
}
