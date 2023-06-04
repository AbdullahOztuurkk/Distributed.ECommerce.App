using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries.Transactions
{
    public class GetTransactionDetailByTransactionIdQuery : IRequest<BaseResponse<TransactionDetailViewModel>>
    {
        public int Id { get; set; }
    }

    public class GetTransactionDetailByTransactionIdQueryHandler : IRequestHandler<GetTransactionDetailByTransactionIdQuery, BaseResponse<TransactionDetailViewModel>>
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

        public async Task<BaseResponse<TransactionDetailViewModel>> Handle(GetTransactionDetailByTransactionIdQuery request, CancellationToken cancellationToken)
        {
            await transactionService.CheckSelfId(request.Id);

            return new SuccessResponse<TransactionDetailViewModel>(await transactionRepository.GetDetailsByTransactionIdAsync(request.Id));
        }
    }
}
