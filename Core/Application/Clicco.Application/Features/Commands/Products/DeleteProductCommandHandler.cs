using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core.ResponseModel;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class DeleteProductCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, BaseResponse>
    {
        private readonly IProductRepository productRepository;
        private readonly IMediator mediator;
        public DeleteProductCommandHandler(IProductRepository productRepository, IMediator mediator)
        {
            this.productRepository = productRepository;
            this.mediator = mediator;
        }
        public async Task<BaseResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await mediator.Send(new GetProductByIdQuery { Id = request.Id });
            if(product == null)
            {
                throw new Exception("Product not found!");
            }
            await productRepository.DeleteAsync(product);
            await productRepository.SaveChangesAsync();
            return new SuccessResponse("Product has been deleted!");
        }
    }
}
