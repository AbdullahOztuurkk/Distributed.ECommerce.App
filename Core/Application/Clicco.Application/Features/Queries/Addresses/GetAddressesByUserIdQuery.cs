using AutoMapper;
using Clicco.Application.Helpers.Contracts;
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
    public class GetAddressesByUserIdQuery : IRequest<BaseResponse<List<AddressViewModel>>>
    {

    }

    public class GetAddressesByUserIdQueryHandler : IRequestHandler<GetAddressesByUserIdQuery, BaseResponse<List<AddressViewModel>>>
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;
        private readonly IClaimHelper claimHelper;
        public GetAddressesByUserIdQueryHandler(IAddressRepository addressRepository, IMapper mapper, IClaimHelper claimHelper)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
            this.claimHelper = claimHelper;
        }
        public async Task<BaseResponse<List<AddressViewModel>>> Handle(GetAddressesByUserIdQuery request, CancellationToken cancellationToken)
        {
            int userId = claimHelper.GetUserId();

            return new SuccessResponse<List<AddressViewModel>>(mapper.Map<List<AddressViewModel>>(await addressRepository.Get(x => x.UserId == userId)));
        }
    }
}
