using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class CreateCouponCommand : IRequest<BaseResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CouponType Type { get; set; }
        public int? TypeId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DiscountType DiscountType { get; set; }
        public int DiscountAmount { get; set; }
    }

    public class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommand, BaseResponse>
    {
        private readonly ICouponRepository couponRepository;
        private readonly IMapper mapper;
        public CreateCouponCommandHandler(ICouponRepository couponRepository, IMapper mapper)
        {
            this.couponRepository = couponRepository;
            this.mapper = mapper;
        }

        public async Task<BaseResponse> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = mapper.Map<Coupon>(request);
            await couponRepository.AddAsync(coupon);
            await couponRepository.SaveChangesAsync();
            return new SuccessResponse("Coupon has been added!");
        }
    }
}
