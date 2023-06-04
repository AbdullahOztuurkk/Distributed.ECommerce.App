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
    public class DeleteVendorCommand : IRequest<BaseResponse<VendorViewModel>>
    {
        public int Id { get; set; }
    }

    public class DeleteVendorCommandHandler : IRequestHandler<DeleteVendorCommand, BaseResponse<VendorViewModel>>
    {
        private readonly IVendorRepository vendorRepository;
        private readonly IMapper mapper;
        private readonly IVendorService vendorService;
        private readonly ICacheManager cacheManager;

        public DeleteVendorCommandHandler(IVendorRepository vendorRepository, IMapper mapper, IVendorService vendorService, ICacheManager cacheManager)
        {
            this.vendorRepository = vendorRepository;
            this.vendorService = vendorService;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }
        
        public async Task<BaseResponse<VendorViewModel>> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
        {
            await vendorService.CheckSelfId(request.Id);

            var vendor = await cacheManager.GetOrSetAsync(CacheKeys.GetSingleKey<Vendor>(request.Id), async () =>
            {
                return await vendorRepository.GetByIdAsync(request.Id);
            });
            vendorRepository.Delete(vendor);
            await vendorRepository.SaveChangesAsync();
            await cacheManager.RemoveAsync(CacheKeys.GetSingleKey<Vendor>(request.Id));
            return new SuccessResponse<VendorViewModel>("Transaction has been deleted!");
        }
    }
}
