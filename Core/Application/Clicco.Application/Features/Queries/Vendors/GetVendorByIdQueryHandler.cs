using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetVendorsByIdQuery : IRequest<BaseResponse<VendorViewModel>>
    {
        public int Id { get; set; }
    }

    public class GetVendorByIdQueryHandler : IRequestHandler<GetVendorsByIdQuery, BaseResponse<VendorViewModel>>
    {
        private readonly IVendorRepository vendorRepository;
        private readonly IMapper mapper;
        private readonly IVendorService vendorService;
        public GetVendorByIdQueryHandler(
            IVendorRepository vendorRepository,
            IMapper mapper,
            IVendorService vendorService)
        {
            this.vendorRepository = vendorRepository;
            this.mapper = mapper;
            this.vendorService = vendorService;
        }
        public async Task<BaseResponse<VendorViewModel>> Handle(GetVendorsByIdQuery request, CancellationToken cancellationToken)
        {
            await vendorService.CheckSelfId(request.Id);

            return new SuccessResponse<VendorViewModel>(mapper.Map<VendorViewModel>(await vendorRepository.GetByIdAsync(request.Id)));
        }
    }
}
