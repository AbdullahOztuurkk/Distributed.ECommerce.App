using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries.Transactions
{
    public class GetTransactionDetailByTransactionIdQuery : IRequest<TransactionDetailViewModel>
    {
        public int Id { get; set; }
    }

    public class GetTransactionDetailByTransactionIdQueryHandler : IRequestHandler<GetTransactionDetailByTransactionIdQuery, TransactionDetailViewModel>
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly ITransactionService transactionService;
        private readonly ICacheManager cacheManager;

        public GetTransactionDetailByTransactionIdQueryHandler(
            ITransactionRepository transactionRepository,
            ITransactionService transactionService,
            ICacheManager cacheManager)
        {
            this.transactionRepository = transactionRepository;
            this.transactionService = transactionService;
            this.cacheManager = cacheManager;
        }

        public async Task<TransactionDetailViewModel> Handle(GetTransactionDetailByTransactionIdQuery request, CancellationToken cancellationToken)
        {
            await transactionService.CheckSelfId(request.Id);

            return await cacheManager.GetOrSetAsync(CacheKeys.GetSingleKey<TransactionDetail>(request.Id), async () =>
            {
                return await transactionRepository.GetDetailsByTransactionIdAsync(request.Id);
            });

        }
    }
}
