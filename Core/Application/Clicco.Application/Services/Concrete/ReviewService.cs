using Clicco.Application.Services.Abstract;
using Clicco.Domain.Model.Dtos.Review;

namespace Clicco.Application.Services.Concrete
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheManager _cacheManager;
        private readonly IUserSessionService _userSessionService;
        public ReviewService(
            IUnitOfWork unitOfWork,
            ICacheManager cacheManager,
            IUserSessionService userSessionService)
        {
            _unitOfWork = unitOfWork;
            _cacheManager = cacheManager;
            _userSessionService = userSessionService;
        }
        public async Task<ResponseDto> Create(CreateReviewDto dto)
        {
            ResponseDto response = new();

            var product = await _unitOfWork.GetRepository<Product>().GetAsync(x => x.Id == dto.ProductId);
            if (product == null)
                return response.Fail(Errors.ProductNotFound);

            Review review = new()
            {
                Description = dto.Description,
                ProductId = product.Id,
                CreatedDate = DateTime.UtcNow.AddHours(3),
                UpdatedDate = null,
                Rating = dto.Rating,
                UserId = _userSessionService.GetUserId(),
            };

            await _unitOfWork.GetRepository<Review>().AddAsync(review);
            await _unitOfWork.GetRepository<Review>().SaveChangesAsync();

            return response;
        }

        public async Task<ResponseDto> Delete(int id)
        {
            ResponseDto response = new();

            var review = await _unitOfWork.GetRepository<Review>().GetByIdAsync(id);
            if (review == null)
                return response.Fail(Errors.ReviewNotFound);

            _unitOfWork.GetRepository<Review>().Delete(review);
            await _unitOfWork.SaveChangesAsync();
            return response;
        }

        public async Task<ResponseDto> Get(int id)
        {
            ResponseDto response = new();

            var review = await _unitOfWork.GetRepository<Review>().GetAsync(x => x.Id == id,
                                                                              x => x.Product);

            if (review == null)
                return response.Fail(Errors.ProductNotFound);

            response.Data = new ReviewResponseDto().Map(review);

            return response;
        }

        public async Task<ResponseDto> GetAllByProductId(int productId, PaginationFilter filter)
        {
            ResponseDto response = new();
            filter.PageNumber = filter.PageNumber > 1 ? filter.PageNumber : 1;
            filter.PageSize = filter.PageNumber > 10 ? filter.PageNumber : 10;

            var reviews = await _unitOfWork.GetRepository<Review>().PaginateAsync(x => x.ProductId == productId, filter,x => x.Product);
            var data = reviews.Select(x => new ReviewResponseDto().Map(x));

            response.Data = data;
            return response;
        }

        public async Task<ResponseDto> Update(UpdateReviewDto dto)
        {
            ResponseDto response = new();
            var review =  await _unitOfWork.GetRepository<Review>().GetByIdAsync(dto.Id);
            if (review == null)
                return response.Fail(Errors.ReviewNotFound);

            var product = await _unitOfWork.GetRepository<Product>().GetByIdAsync(dto.ProductId);
            if (product == null)
                return response.Fail(Errors.ProductNotFound);

            int userId = _userSessionService.GetUserId();
            if (review.UserId != userId)
                return response.Fail(Errors.UnauthorizedOperation);

            review.Rating = dto.Rating;
            review.Description = dto.Description;
            review.UserId = userId;
            review.UpdatedDate = DateTime.UtcNow.AddHours(3);

            _unitOfWork.GetRepository<Review>().Update(review);
            await _unitOfWork.SaveChangesAsync();

            response.Data = new ReviewResponseDto().Map(review);
            return response;
        }
    }
}
