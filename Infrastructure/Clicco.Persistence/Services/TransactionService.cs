using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.Interfaces.Services.External;
using Clicco.Domain.Core.Exceptions;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;

namespace Clicco.Persistence.Services
{
    public class TransactionService : GenericService<Transaction>, ITransactionService
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IAddressRepository addressRepository;
        private readonly ICouponRepository couponRepository;
        private readonly IProductRepository productRepository;
        private readonly IUserService userService;
        public TransactionService(
            IAddressRepository addressRepository,
            ITransactionRepository transactionRepository,
            IUserService userService,
            IProductRepository productRepository,
            ICouponRepository couponRepository)
        {
            this.addressRepository = addressRepository;
            this.transactionRepository = transactionRepository;
            this.userService = userService;
            this.productRepository = productRepository;
            this.couponRepository = couponRepository;
        }

        public async Task CheckAddressIdAsync(int addressId)
        {
            var result = await addressRepository.GetByIdAsync(addressId);
            ThrowExceptionIfNull(result, CustomErrors.AddressNotFound);
        }

        public async Task CheckCouponIdAsync(int couponId)
        {
            var result = await couponRepository.GetByIdAsync(couponId);
            ThrowExceptionIfNull(result, CustomErrors.CouponNotFound);
        }

        public async Task CheckProductIdAsync(int productId)
        {
            var result = await productRepository.GetByIdAsync(productId);
            ThrowExceptionIfNull(result, CustomErrors.ProductNotFound);
        }

        public override async Task CheckSelfId(int entityId, CustomError err = null)
        {
            var result = await transactionRepository.GetByIdAsync(entityId);
            ThrowExceptionIfNull(result, err ?? CustomErrors.TransactionNotFound);
        }

        public async Task CheckUserIdAsync(int userId)
        {
            var result = await userService.IsExistAsync(userId);
            if (!result)
                throw new CustomException(CustomErrors.UserNotFound);
        }

        public async Task<Coupon> GetCouponByIdAsync(int couponId)
        {
            return await couponRepository.GetByIdAsync(couponId);
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await productRepository.GetByIdAsync(productId, x => x.Vendor);
        }
    }
}
