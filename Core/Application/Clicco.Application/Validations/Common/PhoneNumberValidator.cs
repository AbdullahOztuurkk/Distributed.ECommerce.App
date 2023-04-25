using FluentValidation;
using System.Text.RegularExpressions;

namespace Clicco.Application.Validations.Common
{
    public class PhoneNumberValidator : AbstractValidator<string>
    {
        public PhoneNumberValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .MinimumLength(1)
                .MaximumLength(20)
                .WithMessage("Phone number length must between 1 and 20 characters!")
                .Matches(new Regex(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$"))
                .WithMessage("Phone number is not valid.");
        }
    }
}
