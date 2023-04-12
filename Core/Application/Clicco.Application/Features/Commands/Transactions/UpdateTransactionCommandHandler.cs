using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class UpdateTransactionCommand : IRequest<Transaction>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int TotalAmount { get; set; }
        public string Dealer { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public int AddressId { get; set; }
    }
    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, Transaction>
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IMapper mapper;
        private readonly ITransactionService transactionService;
        public UpdateTransactionCommandHandler(ITransactionRepository transactionRepository, IMapper mapper, ITransactionService transactionService)
        {
            this.transactionRepository = transactionRepository;
            this.mapper = mapper;
            this.transactionService = transactionService;
        }
        public async Task<Transaction> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            transactionService.CheckUserIdAsync(request.UserId);
            transactionService.CheckSelfId(request.Id,"Transaction not found!");
            transactionService.CheckAddressIdAsync(request.AddressId);

            var transaction = mapper.Map<Transaction>(request);
            await transactionRepository.AddAsync(transaction);
            await transactionRepository.SaveChangesAsync();
            return transaction;
        }
    }
}
