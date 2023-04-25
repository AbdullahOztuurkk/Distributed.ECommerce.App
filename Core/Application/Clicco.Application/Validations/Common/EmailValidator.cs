using FluentValidation;
using System.Text.RegularExpressions;

namespace Clicco.Application.Validations.Common
{
    public class EmailValidator : AbstractValidator<string>
    {
        public EmailValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("Email is required!")
                .MinimumLength(1)
                .MaximumLength(50)
                .WithMessage("Email length must between 1 and 50 characters!")
                .Matches(new Regex(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b"))
                .WithMessage("Email is not valid!");
        }
    }
}
