using Clicco.AuthAPI.Extensions;
using Clicco.AuthAPI.Models.Request;
using FluentValidation;

namespace Clicco.AuthAPI.Data.Validators
{
    public class UserValidators
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
                RuleFor(x => x.PhoneNumber).MatchPhoneNumberRegex();
            }
        }
    }
}
