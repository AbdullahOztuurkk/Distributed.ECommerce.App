namespace CommerceService.Application.Services.Abstract.External;

public interface IInvoiceService
{
    Task CreateInvoice(string BuyerEmail, Transaction transaction, Product product, Address address);
    Task SendEmailByTransactionId(int transactionId);
}
