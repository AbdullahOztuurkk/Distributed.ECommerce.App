using Clicco.Application.Validations.Common;
using Clicco.Domain.Model.Dtos.Coupon;
using FluentValidation;

namespace Clicco.Application.Validations.Coupons
{
    public class CouponValidators
    {
        public class CreateCouponValidator : AbstractValidator<CreateCouponDto>
        {
            public CreateCouponValidator()
            {
                RuleFor(x => x.Name)
                    .MinimumLength(1)
                    .MaximumLength(50)
                    .NotEmpty();

                RuleFor(x => x.DiscountType).SetValidator(new EnumValidator<DiscountType>());

                RuleFor(x => x.Type).SetValidator(new EnumValidator<CouponType>());

                RuleFor(x => x.Description)
                    .MinimumLength(5)
                    .MaximumLength(200)
                    .NotEmpty();

                RuleFor(x => x.ExpirationDate)
                    .GreaterThan(DateTime.UtcNow)
                    .NotEmpty();

                //RuleFor(x => x.IsActive)
                //    .NotEmpty();

                When(x => x.DiscountType == DiscountType.Percentage, () =>
                {
                    RuleFor(x => x.DiscountAmount)
                        .GreaterThan(0)
                        .LessThanOrEqualTo(100);
                });

                When(x => x.DiscountType == DiscountType.Default, () =>
                {
                    RuleFor(x => x.DiscountAmount)
                        .GreaterThan(5)
                        .NotEmpty();
                });
            }
        }

        //public class DeleteCouponValidator : AbstractValidator<DeleteCouponCommand>
        //{
        //    public DeleteCouponValidator()
        //    {
        //        RuleFor(x => x.Id)
        //            .GreaterThan(0)
        //            .NotEmpty();
        //    }
        //}
    }
}
