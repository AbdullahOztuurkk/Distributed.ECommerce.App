using Clicco.Domain.Model.Dtos.Product;
using FluentValidation;

namespace Clicco.Application.Validations.Products
{
    public class ProductValidators
    {
        public class CreateProductValidator : AbstractValidator<CreateProductDto>
        {
            public CreateProductValidator()
            {
                RuleFor(x => x.Name)
                    .MinimumLength(1)
                    .MaximumLength(50)
                    .NotEmpty();

                RuleFor(x => x.Code)
                    .MinimumLength(1)
                    .MaximumLength(50)
                    .NotEmpty();

                RuleFor(x => x.Description)
                    .MinimumLength(1)
                    .MaximumLength(500)
                    .NotEmpty();

                RuleFor(x => x.UnitPrice)
                    .GreaterThan(0)
                    .NotEmpty();

                RuleFor(x => x.Quantity)
                    .GreaterThan(0)
                    .NotEmpty();
            }
        }

        //public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
        //{
        //    public DeleteProductValidator()
        //    {
        //        RuleFor(x => x.Id)
        //            .GreaterThan(0)
        //            .NotEmpty();
        //    }
        //}
    }
}
