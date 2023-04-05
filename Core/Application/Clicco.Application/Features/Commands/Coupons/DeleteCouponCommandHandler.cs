using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class DeleteCouponCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommand, BaseResponse>
    {
        private readonly ICouponRepository couponRepository;
        private readonly IMediator mediator;
        private readonly ICacheManager cacheManager;
        public DeleteCouponCommandHandler(ICouponRepository couponRepository, IMediator mediator, ICacheManager cacheManager)
        {
            this.couponRepository = couponRepository;
            this.mediator = mediator;
            this.cacheManager = cacheManager;
        }
        public async Task<BaseResponse> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            var couponExist = await mediator.Send(new GetCouponByIdQuery { Id = request.Id }, cancellationToken);
            if (couponExist == null)
            {
                throw new Exception("Coupon not found!");
            }

            var isActive = cacheManager.SearchInArray<int>(CacheKeys.ACTIVE_COUPONS,request.Id);
            
            if (isActive)
            {
                throw new Exception("The coupon is now used!");
            }

            await couponRepository.DeleteAsync(couponExist);
            await couponRepository.SaveChangesAsync();
            return new SuccessResponse("Coupon has been deleted!");
        }
    }
}
