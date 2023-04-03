using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core.ResponseModel;
using MediatR;

namespace Clicco.Application.Features.Commands.Transactions
{
    public class DeleteTransactionCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, BaseResponse>
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IMediator mediator;
        public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository, IMediator mediator)
        {
            this.transactionRepository = transactionRepository;
            this.mediator = mediator;
        }
        public async Task<BaseResponse> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await mediator.Send(new GetTransactionByIdQuery { Id = request.Id }, cancellationToken);
            if (transaction == null)
            {
                throw new Exception("Transaction not found!");
            }
            await transactionRepository.DeleteAsync(transaction);
            await transactionRepository.SaveChangesAsync();
            return new SuccessResponse("Transaction has been created!");
        }
    }
}
