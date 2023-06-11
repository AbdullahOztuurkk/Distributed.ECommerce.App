using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core.ResponseModel;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAddressByIdQuery : IRequest<BaseResponse<AddressViewModel>>
    {
        public int Id { get; set; }
    }

    public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, BaseResponse<AddressViewModel>>
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;
        public GetAddressByIdQueryHandler(IAddressRepository addressRepository, IMapper mapper, ICacheManager cacheManager)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }
        public async Task<BaseResponse<AddressViewModel>> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
        {
            //TODO: Cache
            return new SuccessResponse<AddressViewModel>(mapper.Map<AddressViewModel>(await addressRepository.GetByIdAsync(request.Id)));
        }
    }
}
