using Clicco.Application.Features.Commands;
using Clicco.Application.Features.Commands.Reviews;
using FluentValidation;

namespace Clicco.Application.Validations.Reviews
{
    public class ReviewValidators
    {
        public class CreateReviewValidator : AbstractValidator<CreateReviewCommand>
        {
            public CreateReviewValidator()
            {
                RuleFor(x => x.Description)
                    .MinimumLength(1)
                    .MaximumLength(200)
                    .NotEmpty();

                RuleFor(x => x.CreatedDate)
                    .LessThanOrEqualTo(DateTime.UtcNow)
                    .NotEmpty();

                RuleFor(x => x.Rating)
                    .Must(value => value >= 1 && value <= 5)
                    .NotEmpty();

                RuleFor(x => x.ProductId)
                    .GreaterThan(0)
                    .NotEmpty();

                RuleFor(x => x.UserId)
                    .GreaterThan(0)
                    .NotEmpty();
            }
        }

        public class DeleteReviewValidator : AbstractValidator<DeleteReviewCommand>
        {
            public DeleteReviewValidator()
            {
                RuleFor(x => x.Id)
                    .GreaterThan(0)
                    .NotEmpty();
            }
        }
    }
}
