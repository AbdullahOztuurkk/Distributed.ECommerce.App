using Clicco.Domain.Model.Dtos.Category;
using FluentValidation;

namespace Clicco.Application.Validations.Categories
{
    public class CategoryValidators
    {
        public class CreateCategoryCommandHandler : AbstractValidator<CreateCategoryDto>
        {
            public CreateCategoryCommandHandler()
            {
                RuleFor(x => x.Name)
                    .MinimumLength(3)
                    .MaximumLength(50)
                    .NotEmpty();

                When(x => x.ParentId.HasValue, () =>
                {
                    RuleFor(x => x.ParentId.Value)
                        .GreaterThan(0);
                });

                When(x => x.MenuId.HasValue, () =>
                {
                    RuleFor(x => x.MenuId.Value)
                        .GreaterThan(0);
                });
            }
        }

        //public class DeleteCategoryCommandHandler : AbstractValidator<DeleteCategoryDto>
        //{
        //    public DeleteCategoryCommandHandler()
        //    {
        //        RuleFor(x => x.Id)
        //            .GreaterThan(0)
        //            .NotEmpty();
        //    }
        //}
    }
}
