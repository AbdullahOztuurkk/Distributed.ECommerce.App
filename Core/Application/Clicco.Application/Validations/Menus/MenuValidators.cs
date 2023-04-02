using Clicco.Application.Features.Commands;
using FluentValidation;

namespace Clicco.Application.Validations.Menus
{
    public class MenuValidators
    {
        public class DeleteMenuValidator : AbstractValidator<DeleteMenuCommand>
        {
            public DeleteMenuValidator()
            {
                RuleFor(x => x.Id)
                    .GreaterThan(0)
                    .NotEmpty();
            }
        }
        public class CreateMenuValidator : AbstractValidator<CreateMenuCommand>
        {
            public CreateMenuValidator()
            {
                RuleFor(x => x.CategoryId)
                    .GreaterThan(0)
                    .NotEmpty();
            }
        }
    }
    
}
