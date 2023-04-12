using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.Interfaces.Services.External;
using Clicco.Domain.Model;
using System.Net;

namespace Clicco.Persistence.Services
{
    public class ReviewService : GenericService<Review>, IReviewService
    {
        private readonly IProductRepository productRepository;
        private readonly IUserService userService;
        public ReviewService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async void CheckProductIdAsync(int productId)
        {
            var result = await productRepository.GetByIdAsync(productId);
            ThrowExceptionIfNull(result, "Product not Found!");
        }

        public async void CheckUserIdAsync(int userId)
        {
            var result = await userService.IsExistAsync(userId);
            if (!result)
                throw new Exception("User not found!") { HResult = (int)HttpStatusCode.BadRequest };
        }
    }
}
