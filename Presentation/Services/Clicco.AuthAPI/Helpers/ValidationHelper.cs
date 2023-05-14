using Clicco.AuthServiceAPI.Exceptions;
using FluentValidation;

namespace Clicco.AuthServiceAPI.Helpers
{
    public class ValidationHelper
    {
        public static async Task IsValid<T>(IValidator<T> validator , T value)
        {
            var result = await validator.ValidateAsync(value);
            if(result.Errors.Any())
            {
                throw new AuthException($"Validation Exception: {result.Errors.First()}");
            }
        }
    }
}
