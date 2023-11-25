using Example.EventDriven.Application.SendEvent.Boundaries;
using FluentValidation;

namespace Example.EventDriven.Application.SendEvent
{
    public sealed class SendEventValidator : AbstractValidator<SendEventRequest>
    {
        public SendEventValidator()
        {
            RuleFor(request => request.OperationName).NotEmpty();

            RuleFor(request => request.Value).NotNull();
        }
    }
}
