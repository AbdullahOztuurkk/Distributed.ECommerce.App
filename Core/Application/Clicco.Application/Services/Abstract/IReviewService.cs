using Clicco.Domain.Model.Dtos.Review;

namespace Clicco.Application.Services.Abstract
{
    public interface IReviewService
    {
        Task<ResponseDto> Create(CreateReviewDto dto);
        Task<ResponseDto> Update(UpdateReviewDto dto);
        Task<ResponseDto> Delete(int id);
        Task<ResponseDto> Get(int id);
        Task<ResponseDto> GetAllByProductId(int productId, PaginationFilter filter);
    }
}
