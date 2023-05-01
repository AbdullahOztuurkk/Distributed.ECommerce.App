namespace Clicco.InvoiceServiceAPI.Services.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendInvoiceEmailAsync(object request);
    }
}
