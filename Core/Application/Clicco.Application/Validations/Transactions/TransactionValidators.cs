using Clicco.Application.Features.Commands;
using FluentValidation;

namespace Clicco.Application.Validations.Transactions
{
    public class TransactionValidators
    {
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
