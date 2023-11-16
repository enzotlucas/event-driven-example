using FluentValidation;

namespace Example.EventDriven.Domain.Extensions
{
    public static class ValidatorExtensions
    {
        public static void ThrowIfInvalid<T>(this IValidator<T> validator, T model)
        {
            validator.Validate(model, options =>
            {
                options.ThrowOnFailures();
            });
        }
    }
}
