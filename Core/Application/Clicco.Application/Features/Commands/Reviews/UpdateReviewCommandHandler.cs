using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class UpdateReviewCommand : IRequest<ReviewViewModel>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte Rating { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ReviewViewModel>
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

        public async Task<ReviewViewModel> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            await reviewService.CheckSelfId(request.Id);
            await reviewService.CheckProductIdAsync(request.ProductId);

            var review = mapper.Map<Review>(request);
            reviewRepository.Update(review);
            await reviewRepository.SaveChangesAsync();
            return mapper.Map<ReviewViewModel>(review);
        }
    }
}
