using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class CreateTransactionCommand : IRequest<BaseResponse>
    {
        public string Code { get; set; }
        public int TotalAmount { get; set; }
        public string Dealer { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public int AddressId { get; set; }
    }
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, BaseResponse>
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IMapper mapper;
        private readonly ITransactionService transactionService;
        public CreateTransactionCommandHandler(ITransactionRepository transactionRepository, IMapper mapper, ITransactionService transactionService)
        {
            this.transactionRepository = transactionRepository;
            this.mapper = mapper;
            this.transactionService = transactionService;
        }
        public async Task<BaseResponse> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            transactionService.CheckUserIdAsync(request.UserId);
            transactionService.CheckAddressIdAsync(request.AddressId);

            var transaction = mapper.Map<Transaction>(request);
            await transactionRepository.AddAsync(transaction);
            await transactionRepository.SaveChangesAsync();
            return new SuccessResponse("Transaction has been created!");
        }
    }
}
