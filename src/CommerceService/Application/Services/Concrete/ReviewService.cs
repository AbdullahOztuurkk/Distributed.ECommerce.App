using CommerceService.Application.Services.Abstract;

namespace CommerceService.Application.Services.Concrete;

public class ReviewService : BaseService, IReviewService
{
    public async Task<BaseResponse> Create(CreateReviewRequestDto dto)
    {
        BaseResponse response = new();

        var product = await Db.GetDefaultRepo<Product>().GetAsync(x => x.Id == dto.ProductId);
        if (product == null)
            return response.Fail(Error.E_0000);

        Review review = new()
        {
            Description = dto.Description,
            ProductId = product.Id,
            Rating = dto.Rating,
            UserId = CurrentUser.Id,
        };

        await Db.GetDefaultRepo<Review>().InsertAsync(review);
        await Db.GetDefaultRepo<Review>().SaveChanges();

        return response;
    }

    public async Task<BaseResponse> Delete(int id)
    {
        BaseResponse response = new();

        var review = await Db.GetDefaultRepo<Review>().GetByIdAsync(id);
        if (review == null)
            return response.Fail(Error.E_0000);

        review.DeleteDate = DateTime.UtcNow.AddHours(3);
        review.Status = StatusType.PASSIVE;

        await Db.GetDefaultRepo<Review>().SaveChanges();
        return response;
    }

    public async Task<BaseResponse> Get(int id)
    {
        BaseResponse response = new();

        var review = await Db.GetDefaultRepo<Review>().GetAsync(x => x.Id == id,
                                                                          x => x.Product);

        if (review == null)
            return response.Fail(Error.E_0000);

        response.Data = new ReviewResponseDto().Map(review);

        return response;
    }

    public async Task<BaseResponse> GetAllByProductId(int productId)
    {
        BaseResponse response = new();

        var reviews = await Db.GetDefaultRepo<Review>().GetManyAsync(x => x.ProductId == productId);
        var data = reviews.Select(x => new ReviewResponseDto().Map(x));
        response.Data = data;

        return response;
    }

    public async Task<BaseResponse> Update(UpdateReviewDto dto)
    {
        BaseResponse response = new();
        var review =  await Db.GetDefaultRepo<Review>().GetByIdAsync(dto.Id);
        if (review == null)
            return response.Fail(Error.E_0000);

        var product = await Db.GetDefaultRepo<Product>().GetByIdAsync(dto.ProductId);
        if (product == null)
            return response.Fail(Error.E_0000);

        if (review.UserId != CurrentUser.Id)
            return response.Fail(Error.E_0006);

        review.Rating = dto.Rating;
        review.Description = dto.Description;

        await Db.GetDefaultRepo<Review>().SaveChanges();

        response.Data = new ReviewResponseDto().Map(review);
        return response;
    }
}
