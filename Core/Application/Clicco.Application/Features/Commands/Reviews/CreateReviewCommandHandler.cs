using AutoMapper;
using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.Repositories;
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
        private readonly IMediator mediator;
        public CreateReviewCommandHandler(IReviewRepository reviewRepository, IMapper mapper, IMediator mediator)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
            this.mediator = mediator;
        }
        public async Task<BaseResponse> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            //TODO: Send Request to Auth Api for User Check
            var product = await mediator.Send(new GetProductByIdQuery { Id = request.ProductId });
            if (product == null)
            {
                throw new Exception("Product not found!");
            }
            var review = mapper.Map<Review>(request);
            await reviewRepository.AddAsync(review);
            await reviewRepository.SaveChangesAsync();
            return new SuccessResponse("Review has been created!");
        }
    }
}