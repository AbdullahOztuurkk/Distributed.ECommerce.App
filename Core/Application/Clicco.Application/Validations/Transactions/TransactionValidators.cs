using Clicco.Application.Features.Commands;
using FluentValidation;

namespace Clicco.Application.Validations.Transactions
{
    public class TransactionValidators
    {
        public class CreateTransactionValidator : AbstractValidator<CreateTransactionCommand>
        {
            public CreateTransactionValidator()
            {
                RuleFor(x => x.Code)
                    .MinimumLength(1)
                    .MaximumLength(50)
                    .NotEmpty();

                RuleFor(x => x.Dealer)
                    .MinimumLength(1)
                    .MaximumLength(50)
                    .NotEmpty();

                RuleFor(x => x.CreatedDate)
                    .LessThanOrEqualTo(DateTime.UtcNow)
                    .NotEmpty();

                RuleFor(x => x.DeliveryDate)
                    .NotEmpty();

                RuleFor(x => x.AddressId)
                    .NotEmpty();

                RuleFor(x => x.UserId)
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
