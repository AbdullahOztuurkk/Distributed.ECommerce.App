using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class UpdateProductCommand : IRequest<ProductViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int CategoryId { get; set; }
    }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductViewModel>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IProductService productService;
        private readonly ICacheManager cacheManager;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IProductService productService, ICacheManager cacheManager)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.productService = productService;
            this.cacheManager = cacheManager;
        }

        public async Task<ProductViewModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            await productService.CheckSelfId(request.Id);
            await productService.CheckCategoryId(request.CategoryId);

            var product = mapper.Map<Product>(request);
            productRepository.Update(product);
            await productRepository.SaveChangesAsync();
            return mapper.Map<ProductViewModel>(product);
        }
    }
}
