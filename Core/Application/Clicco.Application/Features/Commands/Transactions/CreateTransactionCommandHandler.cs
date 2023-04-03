using AutoMapper;
using Clicco.Application.Features.Queries.Addresses;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands.Transactions
{
    public class CreateTransactionCommand : IRequest<BaseResponse>
    {
        public string Code { get; set; }
        public int TotalAmount { get; set; }
        public string Dealer { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
    }
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, BaseResponse>
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        public CreateTransactionCommandHandler(ITransactionRepository transactionRepository, IMapper mapper, IMediator mediator)
        {
            this.transactionRepository = transactionRepository;
            this.mapper = mapper;
            this.mediator = mediator;
        }
        public async Task<BaseResponse> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            //TODO: Send Request to Auth Api For User Check
            var address = await mediator.Send(new GetAddressesByUserIdQuery { UserId = request.UserId /*UserId = AuthApiResponse.Id*/},cancellationToken);
            if (address == null)
            {
                throw new Exception("Address not found!");
            }
            var transaction = mapper.Map<Transaction>(request);
            await transactionRepository.AddAsync(transaction);
            await transactionRepository.SaveChangesAsync();
            return new SuccessResponse("Transaction has been added!");
        }
    }
}
