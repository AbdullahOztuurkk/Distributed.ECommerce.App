using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.Interfaces.Services.External;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using Clicco.Domain.Shared.Models.Invoice;
using MediatR;
using static Clicco.Domain.Shared.Global;

namespace Clicco.Application.Features.Commands
{
    public class UpdateTransactionCommand : IRequest<TransactionViewModel>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int TotalAmount { get; set; }
        public string Dealer { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
    }
    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, TransactionViewModel>
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IMapper mapper;
        private readonly ITransactionService transactionService;
        private readonly IRabbitMqService rabbitMqService;
        public UpdateTransactionCommandHandler(
            ITransactionRepository transactionRepository,
            IMapper mapper,
            ITransactionService transactionService,
            IRabbitMqService rabbitMqService)
        {
            this.transactionRepository = transactionRepository;
            this.mapper = mapper;
            this.transactionService = transactionService;
            this.rabbitMqService = rabbitMqService;
        }
        public async Task<TransactionViewModel> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            await transactionService.CheckUserIdAsync(request.UserId);
            await transactionService.CheckSelfId(request.Id);
            await transactionService.CheckAddressIdAsync(request.AddressId);

            var transaction = mapper.Map<Transaction>(request);
            transactionRepository.Update(transaction);
            await transactionRepository.SaveChangesAsync();

            await rabbitMqService.PushMessage<InvoiceTransaction>(new InvoiceTransaction
            {
                Code = request.Code,
                CreatedDate = transaction.CreatedDate,
                Dealer = request.Dealer,
                DeliveryDate = transaction.DeliveryDate,
                DiscountedAmount = transaction.DiscountedAmount,
                TotalAmount = transaction.TotalAmount,
                Id = request.Id,
                TransactionStatus = transaction.TransactionStatus switch
                {
                    TransactionStatus.Failed => "Failed",
                    TransactionStatus.Pending => "Pending",
                    TransactionStatus.Success => "Successful"
                }
            }, QueueNames.UpdatedTransactionQueue);

            return mapper.Map<TransactionViewModel>(transaction);
        }
    }
}
