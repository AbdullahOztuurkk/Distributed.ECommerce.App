using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services
{
    public interface IReviewService : IGenericService<Review>
    {
        Task CheckProductIdAsync(int productId);
    }
}
