using Clicco.AuthAPI.Extensions;
using Clicco.AuthAPI.Models.Request;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Clicco.AuthAPI.Data.Validators
{
    public static class UserValidators
    {
        public class LoginModelValidator : AbstractValidator<LoginDto>
        {
            public LoginModelValidator()
            {
                RuleFor(x => x.Email).MatchEmailRegex();
                RuleFor(x => x.Password).MatchPasswordRegex();
            }
        }

        public class RegisterModelValidator : AbstractValidator<RegisterDto>
        {
            public RegisterModelValidator()
            {
                RuleFor(x => x.Email).MatchPasswordRegex();
                RuleFor(x => x.Password).MatchPasswordRegex();
            }
        }
    }
}
