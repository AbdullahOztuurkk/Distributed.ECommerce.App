using AutoMapper;
using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core.ResponseModel;
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
        public DeleteCouponCommandHandler(ICouponRepository couponRepository, IMediator mediator)
        {
            this.couponRepository = couponRepository;
            this.mediator = mediator;
        }
        public async Task<BaseResponse> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            var couponExist = await mediator.Send(new GetCouponByIdQuery { Id = request.Id}, cancellationToken);
            if (couponExist == null)
            {
                throw new Exception("Coupon not found!");
            }
            //TODO: Check coupon still using from redis service and prevent this
            await couponRepository.DeleteAsync(couponExist);
            await couponRepository.SaveChangesAsync();
            return new SuccessResponse("Coupon has been deleted!");
        }
    }
}
