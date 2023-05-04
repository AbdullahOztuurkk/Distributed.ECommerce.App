using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetProductByIdQuery : IRequest<ProductViewModel>
    {
        public int Id { get; set; }
    }
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductViewModel>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper, ICacheManager cacheManager)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }

        public async Task<ProductViewModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await cacheManager.GetOrSetAsync(CacheKeys.GetSingleKey<TransactionViewModel>(request.Id), async () =>
            {
                return mapper.Map<ProductViewModel>(await productRepository.GetByIdAsync(request.Id, x => x.Category, x => x.Vendor));
            });
        }
    }
}
