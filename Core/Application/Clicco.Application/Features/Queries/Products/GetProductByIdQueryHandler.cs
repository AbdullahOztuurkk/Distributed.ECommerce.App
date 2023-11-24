namespace Clicco.Application.Features.Queries
{
    public class GetProductByIdQuery : IRequest<ResponseDto>
    {
        public int Id { get; set; }
    }
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ResponseDto>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return new SuccessResponse(mapper.Map<ProductResponseDto>(await productRepository.GetByIdAsync(request.Id, x => x.Category, x => x.Vendor)));
        }
    }
}
