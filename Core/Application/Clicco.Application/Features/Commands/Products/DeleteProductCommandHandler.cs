namespace Clicco.Application.Features.Commands
{
    public class DeleteProductCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ResponseDto>
    {
        private readonly IProductRepository productRepository;
        private readonly IProductService productService;
        public DeleteProductCommandHandler(IProductRepository productRepository, IProductService productService)
        {
            this.productRepository = productRepository;
            this.productService = productService;
        }
        public async Task<ResponseDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await productService.CheckById(request.Id);

            var product =  await productRepository.GetByIdAsync(request.Id);
            productRepository.Delete(product);
            await productRepository.SaveChangesAsync();

            return new SuccessResponse("Product has been deleted!");
        }
    }
}
