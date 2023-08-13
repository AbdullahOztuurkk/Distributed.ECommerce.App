using Clicco.Application.Interfaces.Services;
using Clicco.Application.Interfaces.Services.External;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetInvoiceEmailByTransactionIdQuery : IRequest
    {
        public int TransactionId { get; set; }
    }

    public class GetInvoiceEmailByTransactionIdQueryHandler : IRequestHandler<GetInvoiceEmailByTransactionIdQuery>
    {
        private readonly ITransactionService transactionService;
        private readonly IInvoiceService invoiceService;

        public GetInvoiceEmailByTransactionIdQueryHandler(
            ITransactionService transactionService,
            IInvoiceService invoiceService)
        {
            this.transactionService = transactionService;
            this.invoiceService = invoiceService;
        }

        public async Task Handle(GetInvoiceEmailByTransactionIdQuery request, CancellationToken cancellationToken)
        {
            await transactionService.CheckSelfId(request.TransactionId);

            await invoiceService.SendEmailByTransactionId(transactionId: request.TransactionId);
        }
    }
}
