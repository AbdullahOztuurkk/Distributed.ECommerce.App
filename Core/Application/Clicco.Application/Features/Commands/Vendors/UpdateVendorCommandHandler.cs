using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class UpdateVendorCommand : IRequest<BaseResponse<VendorViewModel>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
    }
    public class UpdateVendorCommandHandler : IRequestHandler<UpdateVendorCommand, BaseResponse<VendorViewModel>>
    {
        private readonly IVendorRepository vendorRepository;
        private readonly IMapper mapper;
        private readonly IVendorService vendorService;
        private readonly ICacheManager cacheManager;
        public UpdateVendorCommandHandler(IVendorRepository vendorRepository, IMapper mapper, IVendorService vendorService, ICacheManager cacheManager)
        {
            this.vendorRepository = vendorRepository;
            this.mapper = mapper;
            this.vendorService = vendorService;
            this.cacheManager = cacheManager;
        }
        public async Task<BaseResponse<VendorViewModel>> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
        {
            await vendorService.CheckSelfId(request.Id);

            var vendor = await cacheManager.GetOrSetAsync(CacheKeys.GetSingleKey<Vendor>(request.Id), async () =>
            {
                return await vendorRepository.GetByIdAsync(request.Id);
            });

            vendorRepository.Update(mapper.Map(request, vendor));
            await vendorRepository.SaveChangesAsync();
            await cacheManager.SetAsync(CacheKeys.GetSingleKey<Vendor>(request.Id), vendor);

            return new SuccessResponse<VendorViewModel>(mapper.Map<VendorViewModel>(vendor));
        }
    }
}
