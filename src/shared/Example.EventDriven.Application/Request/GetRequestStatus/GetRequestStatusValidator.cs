﻿using Example.EventDriven.Application.GetRequestStatus.Boundaries;
using FluentValidation;

namespace Example.EventDriven.Application.GetRequestStatus
{
    public sealed class GetRequestStatusValidator : AbstractValidator<GetRequestStatusRequest>
    {
        public GetRequestStatusValidator()
        {
            RuleFor(request => request.RequestId)
                .NotEmpty();
        }
    }
}
