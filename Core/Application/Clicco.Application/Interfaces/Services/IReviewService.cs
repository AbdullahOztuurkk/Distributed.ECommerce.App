using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services
{
    public interface IReviewService : IGenericService<Review>
    {
        void CheckProductId(int productId);
        void CheckUserId(int userId);
    }
}
