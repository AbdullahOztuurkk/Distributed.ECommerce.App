namespace CommerceService.Application.Services.Abstract;

public interface IReviewService
{
    Task<BaseResponse> Create(CreateReviewRequestDto dto);
    Task<BaseResponse> Update(UpdateReviewDto dto);
    Task<BaseResponse> Delete(int id);
    Task<BaseResponse> Get(int id);
    Task<BaseResponse> GetAllByProductId(int productId);
}
