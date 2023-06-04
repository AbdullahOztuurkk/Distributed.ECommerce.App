using AutoMapper;
using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class DeleteProductCommand : IRequest<BaseResponse<ProductViewModel>>
    {
        public int Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, BaseResponse<ProductViewModel>>
    {
        private readonly IProductRepository productRepository;
        private readonly IProductService productService;
        private readonly ICacheManager cacheManager;
        public DeleteProductCommandHandler(IProductRepository productRepository, IProductService productService, ICacheManager cacheManager)
        {
            this.productRepository = productRepository;
            this.productService = productService;
            this.cacheManager = cacheManager;
        }
        public async Task<BaseResponse<ProductViewModel>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await productService.CheckSelfId(request.Id);

            var product = await cacheManager.GetOrSetAsync(CacheKeys.GetSingleKey<Product>(request.Id), async () =>
            {
                return await productRepository.GetByIdAsync(request.Id);
            });

            productRepository.Delete(product);
            await productRepository.SaveChangesAsync();
            return new SuccessResponse<ProductViewModel>("Product has been deleted!");
        }
    }
}
