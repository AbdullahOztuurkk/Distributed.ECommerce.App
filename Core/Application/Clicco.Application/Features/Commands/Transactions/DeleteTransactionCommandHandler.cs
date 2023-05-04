using AutoMapper;
using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class DeleteTransactionCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, BaseResponse>
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IMapper mapper;
        private readonly ITransactionService transactionService;
        private readonly ICacheManager cacheManager;
        public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository, IMapper mapper, ITransactionService transactionService, ICacheManager cacheManager)
        {
            this.transactionRepository = transactionRepository;
            this.transactionService = transactionService;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }
        public async Task<BaseResponse> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            await transactionService.CheckSelfId(request.Id);

            var transaction = mapper.Map<Transaction>(request);
            transactionRepository.Delete(transaction);
            await transactionRepository.SaveChangesAsync();
            await cacheManager.RemoveAsync(CacheKeys.GetSingleKey<TransactionViewModel>(request.Id));
            return new SuccessResponse("Transaction has been deleted!");
        }
    }
}
