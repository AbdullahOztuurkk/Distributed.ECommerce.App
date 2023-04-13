using FluentValidation;
using System.Text.RegularExpressions;

namespace Clicco.AuthAPI.Extensions
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> MatchEmailRegex<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                    .NotEmpty()
                    .WithMessage("Email is required.")
                    .Matches(new Regex(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b"))
                    .WithMessage("Email is not valid.");
        }
        public static IRuleBuilderOptions<T, string> MatchPasswordRegex<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage("Password is required.")
                .Matches(new Regex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"))
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character, and be at least 8 characters long.");
        }

        public static IRuleBuilderOptions<T, string> MatchPhoneNumberRegex<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .Matches(new Regex(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$"))
                .WithMessage("Phone number is not valid.");
        }
    }
}
