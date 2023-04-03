using Clicco.Application.Features.Queries.Addresses;
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
        private readonly IMediator mediator;
        public DeleteAddressCommandHandler(IAddressRepository addressRepository, IMediator mediator)
        {
            this.addressRepository = addressRepository;
            this.mediator = mediator;
        }
        public async Task<BaseResponse> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {  
            var address = await mediator.Send(new GetAddressByIdQuery { Id = request.Id });
            if(address == null)
            {
                throw new Exception("Address not found!");
            }
            await addressRepository.DeleteAsync(address);
            return new SuccessResponse("Address has been deleted!");
        }
    }
}
