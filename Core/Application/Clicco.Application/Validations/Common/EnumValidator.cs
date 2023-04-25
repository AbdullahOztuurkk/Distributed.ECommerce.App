using FluentValidation;

namespace Clicco.Application.Validations.Common
{
    public class EnumValidator<TEnum> : AbstractValidator<TEnum> where TEnum : Enum
    {
        public EnumValidator()
        {
            RuleFor(x => x)
                .Must(x => Enum.IsDefined(typeof(TEnum), x))
                .WithMessage($"Invalid value for {typeof(TEnum).Name}!");
        }
    }
}
