namespace Clicco.Application.Features.Commands
{
    public class UpdateProductCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int CategoryId { get; set; }
    }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ResponseDto>
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

        public async Task<ResponseDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            await productService.CheckById(request.Id);
            await productService.CheckCategoryId(request.CategoryId);

            var product = await productRepository.GetByIdAsync(request.Id);
            productRepository.Update(mapper.Map(request, product));
            await productRepository.SaveChangesAsync();

            return new SuccessResponse(mapper.Map<ProductResponseDto>(product));
        }
    }
}
