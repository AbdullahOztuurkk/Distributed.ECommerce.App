using AutoMapper;
using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
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
        private readonly IMapper mapper;
        private readonly IProductService productService;
        public DeleteProductCommandHandler(IProductRepository productRepository, IMapper mapper, IProductService productService)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.productService = productService;
        }
        public async Task<BaseResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await productService.CheckSelfId(request.Id);

            var product = mapper.Map<Product>(request);
            productRepository.Delete(product);
            await productRepository.SaveChangesAsync();
            return new SuccessResponse("Product has been deleted!");
        }
    }
}
