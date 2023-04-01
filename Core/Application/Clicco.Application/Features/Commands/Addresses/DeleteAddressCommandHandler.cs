using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;
using static Clicco.Domain.Core.ResponseModel.BaseResponse;

namespace Clicco.Application.Features.Commands
{
    public class DeleteAddressCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, BaseResponse>
    {
        private readonly IAddressRepository addressRepository;
        public DeleteAddressCommandHandler(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }
        public async Task<BaseResponse> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {  
            var address = new Address { Id = request.Id };
            await addressRepository.DeleteAsync(address);
            return new SuccessResponse("Address has been deleted!");

        }
    }
}
