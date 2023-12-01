using Example.EventDriven.Application.Request.SendRequest.Boundaries;
using FluentValidation;

namespace Example.EventDriven.Application.Request.SendRequest
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
