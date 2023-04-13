using AutoMapper;
using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class UpdateReviewCommand : IRequest<Review>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte Rating { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Review>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;
        private readonly IReviewService reviewService;

        public UpdateReviewCommandHandler(IReviewRepository reviewRepository, IMapper mapper, IReviewService reviewService)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
            this.reviewService = reviewService;
        }

        public async Task<Review> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            await reviewService.CheckSelfId(request.Id);
            await reviewService.CheckProductIdAsync(request.ProductId);

            var review = mapper.Map<Review>(request);
            reviewRepository.Update(review);
            await reviewRepository.SaveChangesAsync();
            return review;
        }
    }
}
