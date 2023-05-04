using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAllProductQuery : IRequest<List<ProductViewModel>>
    {

    }

    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, List<ProductViewModel>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;

        public GetAllProductQueryHandler(
            IProductRepository productRepository,
            IMapper mapper,
            ICacheManager cacheManager)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }

        public async Task<List<ProductViewModel>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            return await cacheManager.GetOrSetAsync(CacheKeys.GetListKey<TransactionViewModel>(), async () =>
            {
                return mapper.Map<List<ProductViewModel>>(await productRepository.Get(filter: null, x => x.Category, x => x.Vendor));
            });
        }
    }
}
