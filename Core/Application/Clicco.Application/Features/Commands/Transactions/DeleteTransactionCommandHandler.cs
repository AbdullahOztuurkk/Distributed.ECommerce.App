using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.Interfaces.Services.External;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;
using static Clicco.Domain.Shared.Global;

namespace Clicco.Application.Features.Commands
{
    public class DeleteTransactionCommand : IRequest<BaseResponse<TransactionViewModel>>
    {
        public int Id { get; set; }
    }

    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, BaseResponse<TransactionViewModel>>
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly ITransactionService transactionService;
        private readonly IRabbitMqService rabbitMqService;
        public DeleteTransactionCommandHandler(
            ITransactionRepository transactionRepository,
            ITransactionService transactionService,
            IRabbitMqService rabbitMqService)
        {
            this.transactionRepository = transactionRepository;
            this.transactionService = transactionService;
            this.rabbitMqService = rabbitMqService;
        }
        public async Task<BaseResponse<TransactionViewModel>> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            await transactionService.CheckSelfId(request.Id);

            var transaction =  await transactionRepository.GetByIdAsync(request.Id);
            transactionRepository.Delete(transaction);
            await transactionRepository.SaveChangesAsync();

            await rabbitMqService.PushMessage(request.Id, QueueNames.DeletedTransactionQueue);

            return new SuccessResponse<TransactionViewModel>("Transaction has been deleted!");
        }
    }
}
