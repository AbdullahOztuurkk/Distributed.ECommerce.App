using Clicco.Application.Validations.Common;
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

                RuleFor(x => x.Email).SetValidator(new EmailValidator());

                RuleFor(x => x.PhoneNumber).SetValidator(new PhoneNumberValidator());

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
