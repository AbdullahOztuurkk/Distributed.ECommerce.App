using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;

namespace Clicco.Persistence.Services
{
    public class ReviewService : GenericService<Review>, IReviewService
    {
        private readonly IProductRepository productRepository;
        public ReviewService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async void CheckProductId(int productId)
        {
            var result = await productRepository.GetByIdAsync(productId);
            ThrowExceptionIfNull(result, "Product not Found!");
        }

        public void CheckUserId(int userId)
        {
            //TODO: Inject Auth Service then send request to Auth Api for check user Id
            throw new NotImplementedException();
        }
    }
}
