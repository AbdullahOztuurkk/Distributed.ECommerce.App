using AutoMapper;
using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class UpdateProductCommand : IRequest<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int CategoryId { get; set; }
    }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IProductService productService;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IProductService productService)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.productService = productService;
        }

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            await productService.CheckSelfId(request.Id);
            await productService.CheckCategoryId(request.CategoryId);

            var product = mapper.Map<Product>(request);
            productRepository.Update(product);
            await productRepository.SaveChangesAsync();
            return product;
        }
    }
}
