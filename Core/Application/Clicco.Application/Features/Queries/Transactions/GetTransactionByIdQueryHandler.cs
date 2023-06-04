using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetTransactionByIdQuery : IRequest<BaseResponse<TransactionViewModel>>
    {
        public int Id { get; set; }
    }

    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, BaseResponse<TransactionViewModel>>
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
        public async Task<BaseResponse<TransactionViewModel>> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            await transactionService.CheckSelfId(request.Id);

            return new SuccessResponse<TransactionViewModel>(mapper.Map<TransactionViewModel>(await transactionRepository.GetByIdAsync(request.Id)));
        }
    }
}
