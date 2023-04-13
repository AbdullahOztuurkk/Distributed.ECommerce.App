using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.Interfaces.Services.External;
using Clicco.Domain.Core.Exceptions;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;

namespace Clicco.Persistence.Services
{
    public class ReviewService : GenericService<Review>, IReviewService
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IProductRepository productRepository;
        private readonly IUserService userService;
        public ReviewService(IProductRepository productRepository, IReviewRepository reviewRepository, IUserService userService)
        {
            this.productRepository = productRepository;
            this.reviewRepository = reviewRepository;
            this.userService = userService;
        }
        public async Task CheckProductIdAsync(int productId)
        {
            var result = await productRepository.GetByIdAsync(productId);
            ThrowExceptionIfNull(result, CustomErrors.ProductNotFound);
        }

        public override async Task CheckSelfId(int entityId, CustomError? err = null)
        {
            var result = await reviewRepository.GetByIdAsync(entityId);
            ThrowExceptionIfNull(result, err ?? CustomErrors.ReviewNotFound);
        }

        public async Task CheckUserIdAsync(int userId)
        {
            var result = await userService.IsExistAsync(userId);
            if (!result)
                throw new CustomException(CustomErrors.UserNotFound);
        }
    }
}
