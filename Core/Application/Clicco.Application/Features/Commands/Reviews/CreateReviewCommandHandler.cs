using AutoMapper;
using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class CreateReviewCommand : IRequest<BaseResponse>
    {
        public string Description { get; set; }
        public byte Rating { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, BaseResponse>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;
        private readonly IReviewService reviewService;
        public CreateReviewCommandHandler(IReviewRepository reviewRepository, IMapper mapper, IReviewService reviewService)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
            this.reviewService = reviewService;
        }
        public async Task<BaseResponse> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            await reviewService.CheckUserIdAsync(request.UserId);
            await reviewService.CheckProductIdAsync(request.ProductId);

            var review = mapper.Map<Review>(request);
            await reviewRepository.AddAsync(review);
            await reviewRepository.SaveChangesAsync();
            return new SuccessResponse("Review has been created!");
        }
    }
}