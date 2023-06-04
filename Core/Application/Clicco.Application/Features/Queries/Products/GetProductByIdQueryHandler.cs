using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetProductByIdQuery : IRequest<BaseResponse<ProductViewModel>>
    {
        public int Id { get; set; }
    }
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, BaseResponse<ProductViewModel>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<ProductViewModel>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return new SuccessResponse<ProductViewModel>(mapper.Map<ProductViewModel>(await productRepository.GetByIdAsync(request.Id, x => x.Category, x => x.Vendor)));
        }
    }
}
