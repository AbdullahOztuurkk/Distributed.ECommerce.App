using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;

namespace Clicco.Persistence.Services
{
    public class CouponService : GenericService<Coupon>, ICouponService
    {
        private readonly ICouponRepository couponRepository;

        public CouponService(ICouponRepository couponRepository)
        {
            this.couponRepository = couponRepository;
        }

        public override async Task CheckSelfId(int entityId, CustomError err = null)
        {
            var result = await couponRepository.GetByIdAsync(entityId);
            ThrowExceptionIfNull(result, err ?? CustomErrors.CouponNotFound);
        }
    }
}
