using Clicco.Domain.Model.Dtos.Menu;
using FluentValidation;

namespace Clicco.Application.Validations.Menus
{
    public class MenuValidators
    {
        //public class DeleteMenuValidator : AbstractValidator<DeleteMenuCommand>
        //{
        //    public DeleteMenuValidator()
        //    {
        //        RuleFor(x => x.Id)
        //            .GreaterThan(0)
        //            .NotEmpty();
        //    }
        //}
        public class CreateMenuValidator : AbstractValidator<CreateMenuDto>
        {
            public CreateMenuValidator()
            {
                RuleFor(x => x.CategoryId)
                    .GreaterThan(0)
                    .NotEmpty();

                RuleFor(x => x.Name)
                    .MinimumLength(1)
                    .MaximumLength(50)
                    .NotEmpty();
            }
        }
    }

}
