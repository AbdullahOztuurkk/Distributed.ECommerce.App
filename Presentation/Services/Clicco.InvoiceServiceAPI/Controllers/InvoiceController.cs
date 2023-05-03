using Clicco.Domain.Shared.Models.Invoice;
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
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceRequest invoice)
        {
            var result = await invoiceService.CreateAsync(invoice);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("SendInvoiceEmail/{id}")]
        public async Task<IActionResult> SendInvoiceEmail([FromRoute] int id)
        {
            var result = await invoiceService.SendInvoiceEmail(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}