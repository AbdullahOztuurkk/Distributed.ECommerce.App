using FluentValidation;

namespace Clicco.Application.Validations.Transactions
{
    public class TransactionValidators
    {
        public class CreateTransactionValidator : AbstractValidator<CreateTransactionDto>
        {
            public CreateTransactionValidator()
            {
                RuleFor(x => x.BankId)
                    .NotEmpty();

                RuleFor(x => x.ProductId)
                    .NotEmpty();

                RuleFor(x => x.CardInformation)
                    .ChildRules((validator) =>
                    {
                        validator.RuleFor(x => x.CardNumber).NotEmpty();
                        validator.RuleFor(x => x.ExpirationDate).NotEmpty();
                        validator.RuleFor(x => x.CardSecurityNumber).NotEmpty();
                        validator.RuleFor(x => x.CardOwner).NotEmpty();
                    });

                RuleFor(x => x.Quantity)
                    .NotEmpty();

                RuleFor(x => x.AddressId)
                    .NotEmpty();
            }
        }

        public class DeleteTransactionValidator : AbstractValidator<DeleteTransactionCommand>
        {
            public DeleteTransactionValidator()
            {
                RuleFor(x => x.Id)
                    .NotEmpty();
            }
        }
    }
}
