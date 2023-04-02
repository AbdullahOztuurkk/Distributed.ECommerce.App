using AutoMapper;
using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class CreateProductCommand : IRequest<BaseResponse>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int CategoryId { get; set; }

    }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, BaseResponse>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IMediator mediator)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.mediator = mediator;
        }
        public async Task<BaseResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var category = await mediator.Send(new GetCategoryByIdQuery { Id = request.CategoryId }, cancellationToken);
            if(category != null)
            {
                var product = mapper.Map<Product>(request);
                //product.SlugUrl = product.Name.ToSeoFriendlyUrl();
                await productRepository.AddAsync(product);
                await productRepository.SaveChangesAsync();
                return new SuccessResponse("Product has been added!");
            }
            return new ErrorResponse("Invalid Category!");
        }
    }
}
