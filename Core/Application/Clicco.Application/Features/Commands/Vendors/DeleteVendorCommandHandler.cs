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
        private readonly IVendorService vendorService;

        public DeleteVendorCommandHandler(IVendorRepository vendorRepository, IVendorService vendorService)
        {
            this.vendorRepository = vendorRepository;
            this.vendorService = vendorService;
        }
        
        public async Task<BaseResponse<VendorViewModel>> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
        {
            await vendorService.CheckSelfId(request.Id);

            var vendor = await vendorRepository.GetByIdAsync(request.Id);
            vendorRepository.Delete(vendor);
            await vendorRepository.SaveChangesAsync();

            return new SuccessResponse<VendorViewModel>("Transaction has been deleted!");
        }
    }
}
