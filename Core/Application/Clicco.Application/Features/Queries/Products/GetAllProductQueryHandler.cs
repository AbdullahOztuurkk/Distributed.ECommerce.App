using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAllProductQuery : IRequest<List<Product>>
    {

    }

    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, List<Product>>
    {
        private readonly IProductRepository productRepository;

        public GetAllProductQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<List<Product>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            return await productRepository.Get(null, x => x.Category, x => x.Vendor);
        }
    }
}
