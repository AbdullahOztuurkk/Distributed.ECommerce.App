using Clicco.Application.Features.Commands;
using FluentValidation;

namespace Clicco.Application.Validations.Transactions
{
    public class VendorValidators
    {
        public class CreateVendorValidator : AbstractValidator<CreateVendorCommand>
        {
            public CreateVendorValidator()
            {
                RuleFor(x => x.Address)
                    .MinimumLength(1)
                    .MaximumLength(100)
                    .NotEmpty();

                RuleFor(x => x.Email)
                    .MinimumLength(1)
                    .MaximumLength(50)
                    .NotEmpty();

                RuleFor(x => x.PhoneNumber)
                    .MaximumLength(20)
                    .NotEmpty();

                RuleFor(x => x.Region)
                    .NotEmpty();

                RuleFor(x => x.Name)
                    .MinimumLength(1)
                    .MaximumLength(50)
                    .NotEmpty();
            }
        }
        public class DeleteVendorValidator : AbstractValidator<DeleteVendorCommand>
        {
            public DeleteVendorValidator()
            {
                RuleFor(x => x.Id)
                    .NotEmpty();
            }
        }
    }
}
