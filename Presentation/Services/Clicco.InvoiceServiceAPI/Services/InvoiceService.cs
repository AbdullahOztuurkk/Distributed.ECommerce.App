using Clicco.Domain.Shared.Models.Invoice;
using Clicco.InvoiceServiceAPI.Data.Models;
using Clicco.InvoiceServiceAPI.Data.Repositories.Contracts;
using Clicco.InvoiceServiceAPI.Services.Contracts;

namespace Clicco.InvoiceServiceAPI.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IEmailService emailService;

        public InvoiceService(IInvoiceRepository invoiceRepository, IEmailService emailService)
        {
            this.invoiceRepository = invoiceRepository;
            this.emailService = emailService;
        }

        public async Task<InvoiceResult> CreateAsync(CreateInvoiceRequest invoice)
        {
            var invoiceModel = new Invoice
            {
                Address = invoice.Address,
                Coupon = invoice.Coupon,
                Product = invoice.Product,
                Transaction = invoice.Transaction,
                Vendor = invoice.Vendor,
                BuyerEmail = invoice.BuyerEmail,
            };

            var result = await invoiceRepository.CreateAsync(invoiceModel);
            return result != null ? new SuccessInvoiceResult() : new FailedInvoiceResult("Error Occurred!");
        }

        public async Task<InvoiceResult> SendInvoiceEmail(int transactionId)
        {
            var invoice = await GetByTransactionIdAsync(transactionId);
            if ( invoice != null)
            {
                await emailService.SendInvoiceEmailAsync(invoice);
                return new SuccessInvoiceResult($"Invoice email sent to {invoice.BuyerEmail}");
            }
            return new FailedInvoiceResult("Invoice cannot be found!");
        }

        #region Private methods
        
        private async Task<Invoice> GetByTransactionIdAsync(int transactionId)
        {
            Invoice result = await invoiceRepository.FindOneAsync(x => x.Transaction.Id == transactionId);
            return result;
        }

        #endregion
    }
}
