namespace Clicco.Application.Features.Commands
{
    public class CreateProductCommand : IRequest<ResponseDto>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public int VendorId { get; set; }

    }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ResponseDto>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IProductService productService;
        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IProductService productService)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.productService = productService;
        }
        public async Task<ResponseDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await productService.CheckCategoryId(request.CategoryId);
            await productService.CheckVendorId(request.VendorId);

            var product = mapper.Map<Product>(request);
            await productRepository.AddAsync(product);
            await productRepository.SaveChangesAsync();
            return new SuccessResponse("Product has been created!");
        }
    }
}
