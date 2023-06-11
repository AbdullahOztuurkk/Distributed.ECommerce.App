using Clicco.Application.Features.Commands;
using FluentValidation;

namespace Clicco.Application.Validations.Addresses
{
    public class AddressValidators
    {
        public class CreateAddressValidator : AbstractValidator<CreateAddressCommand>
        {
            public CreateAddressValidator()
            {
                RuleFor(x => x.Street)
                    .MinimumLength(5)
                    .MaximumLength(50)
                    .NotEmpty();

                RuleFor(x => x.State)
                    .MinimumLength(5)
                    .MaximumLength(50)
                    .NotEmpty();

                RuleFor(x => x.City)
                    .MinimumLength(5)
                    .MaximumLength(50)
                    .NotEmpty();

                RuleFor(x => x.Country)
                    .MinimumLength(5)
                    .MaximumLength(50)
                    .NotEmpty();

                RuleFor(x => x.ZipCode)
                    .MinimumLength(5)
                    .MaximumLength(20)
                    .NotEmpty();
            }
        }

        public class DeleteAddressValidator : AbstractValidator<DeleteAddressCommand>
        {
            public DeleteAddressValidator()
            {
                RuleFor(x => x.Id)
                    .GreaterThan(0)
                    .NotEmpty();
            }
        }
    }
}
