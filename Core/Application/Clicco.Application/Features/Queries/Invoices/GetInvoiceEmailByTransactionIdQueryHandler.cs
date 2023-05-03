using Clicco.Application.Interfaces.Services;
using Clicco.Application.Interfaces.Services.External;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetInvoiceEmailByTransactionIdQuery : IRequest<bool>
    {
        public int TransactionId { get; set; }
    }

    public class GetInvoiceEmailByTransactionIdQueryHandler : IRequestHandler<GetInvoiceEmailByTransactionIdQuery, bool>
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

        public async Task<bool> Handle(GetInvoiceEmailByTransactionIdQuery request, CancellationToken cancellationToken)
        {
            await transactionService.CheckSelfId(request.TransactionId);

            return await invoiceService.SendEmailByTransactionId(transactionId: request.TransactionId);
        }
    }
}
