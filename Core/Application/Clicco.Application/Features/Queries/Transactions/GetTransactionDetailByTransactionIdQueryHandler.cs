using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
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

        public GetTransactionDetailByTransactionIdQueryHandler(ITransactionRepository transactionRepository, ITransactionService transactionService)
        {
            this.transactionRepository = transactionRepository;
            this.transactionService = transactionService;
        }

        public async Task<TransactionDetailViewModel> Handle(GetTransactionDetailByTransactionIdQuery request, CancellationToken cancellationToken)
        {
            await transactionService.CheckSelfId(request.Id);

            return await transactionRepository.GetDetailsByTransactionIdAsync(request.Id);
        }
    }
}
