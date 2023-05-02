using Clicco.Domain.Shared.Models.Invoice;
using Clicco.InvoiceServiceAPI.Data.Models;
using Clicco.InvoiceServiceAPI.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Clicco.InvoiceServiceAPI.Controllers
{
    [ApiController]
    [Route("api/invoices")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateInvoice(CreateInvoiceRequest invoice)
        {
            var result = await invoiceService.CreateAsync(invoice);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("SendInvoiceEmail/{transactionId}")]
        public async Task<IActionResult> SendInvoiceEmail(int transactionId)
        {
            var result = await invoiceService.SendInvoiceEmail(transactionId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}