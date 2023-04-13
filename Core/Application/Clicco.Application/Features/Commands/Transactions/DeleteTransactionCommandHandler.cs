using AutoMapper;
using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
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
        public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository, IMapper mapper, ITransactionService transactionService)
        {
            this.transactionRepository = transactionRepository;
            this.transactionService = transactionService;
            this.mapper = mapper;
        }
        public async Task<BaseResponse> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            await transactionService.CheckSelfId(request.Id);

            var transaction = mapper.Map<Transaction>(request);
            transactionRepository.Delete(transaction);
            await transactionRepository.SaveChangesAsync();
            return new SuccessResponse("Transaction has been deleted!");
        }
    }
}
