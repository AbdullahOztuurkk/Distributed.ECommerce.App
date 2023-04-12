using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services
{
    public interface IReviewService : IGenericService<Review>
    {
        void CheckProductIdAsync(int productId);
        void CheckUserIdAsync(int userId);
    }
}
