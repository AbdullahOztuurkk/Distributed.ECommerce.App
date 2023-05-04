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
    public class CreateProductCommand : IRequest<BaseResponse>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public int VendorId { get; set; }

    }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, BaseResponse>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IProductService productService;
        private readonly ICacheManager cacheManager;
        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IProductService productService, ICacheManager cacheManager)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.productService = productService;
            this.cacheManager = cacheManager;
        }
        public async Task<BaseResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await productService.CheckCategoryId(request.CategoryId);
            await productService.CheckVendorId(request.VendorId);

            var product = mapper.Map<Product>(request);
            //product.SlugUrl = product.Name.ToSeoFriendlyUrl();
            await productRepository.AddAsync(product);
            await productRepository.SaveChangesAsync();
            await cacheManager.RemoveAsync(CacheKeys.GetListKey<ProductViewModel>());
            return new SuccessResponse("Product has been created!");
        }
    }
}
