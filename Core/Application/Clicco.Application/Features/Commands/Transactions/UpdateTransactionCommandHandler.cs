using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class UpdateTransactionCommand : IRequest<TransactionViewModel>
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
    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, TransactionViewModel>
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IMapper mapper;
        private readonly ITransactionService transactionService;
        private readonly ICacheManager cacheManager;
        public UpdateTransactionCommandHandler(ITransactionRepository transactionRepository, IMapper mapper, ITransactionService transactionService, ICacheManager cacheManager)
        {
            this.transactionRepository = transactionRepository;
            this.mapper = mapper;
            this.transactionService = transactionService;
            this.cacheManager = cacheManager;
        }
        public async Task<TransactionViewModel> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            await transactionService.CheckUserIdAsync(request.UserId);
            await transactionService.CheckSelfId(request.Id);
            await transactionService.CheckAddressIdAsync(request.AddressId);

            var transaction = mapper.Map<Transaction>(request);
            await transactionRepository.AddAsync(transaction);
            await transactionRepository.SaveChangesAsync();
            return mapper.Map<TransactionViewModel>(transaction);
        }
    }
}
