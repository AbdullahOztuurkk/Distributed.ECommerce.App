using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class DeleteVendorCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteVendorCommandHandler : IRequestHandler<DeleteVendorCommand, BaseResponse>
    {
        private readonly IVendorRepository vendorRepository;
        private readonly IMapper mapper;
        private readonly IVendorService vendorService;
        public DeleteVendorCommandHandler(IVendorRepository vendorRepository, IMapper mapper, IVendorService vendorService)
        {
            this.vendorRepository = vendorRepository;
            this.vendorService = vendorService;
            this.mapper = mapper;
        }
        public async Task<BaseResponse> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
        {
            await vendorService.CheckSelfId(request.Id);

            var vendor = mapper.Map<Vendor>(request);
            vendorRepository.Delete(vendor);
            await vendorRepository.SaveChangesAsync();
            return new SuccessResponse("Transaction has been deleted!");
        }
    }
}
