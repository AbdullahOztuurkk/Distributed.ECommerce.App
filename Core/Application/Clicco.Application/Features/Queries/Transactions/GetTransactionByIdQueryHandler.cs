using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetTransactionByIdQuery : IRequest<TransactionViewModel>
    {
        public int Id { get; set; }
    }

    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionViewModel>
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly ITransactionService transactionService;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;
        public GetTransactionByIdQueryHandler(
            ITransactionRepository transactionRepository,
            IMapper mapper,
            ITransactionService transactionService,
            ICacheManager cacheManager)
        {
            this.transactionRepository = transactionRepository;
            this.mapper = mapper;
            this.transactionService = transactionService;
            this.cacheManager = cacheManager;
        }
        public async Task<TransactionViewModel> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            await transactionService.CheckSelfId(request.Id);

            return await cacheManager.GetOrSetAsync(CacheKeys.GetSingleKey<Transaction>(request.Id), async () =>
            {
                return mapper.Map<TransactionViewModel>(await transactionRepository.GetByIdAsync(request.Id));
            });
        }
    }
}
