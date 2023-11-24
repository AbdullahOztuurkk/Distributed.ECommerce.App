using Clicco.Application.Services.Abstract;
using Clicco.Domain.Shared.Models.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clicco.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public PaymentController(ITransactionService transactionService)
        {
            this._transactionService = transactionService;
        }

        [HttpPost]
        [Route("pay")]
        public async Task<IActionResult> Pay(CreateTransactionDto paymentRequest)
        {
            var response = await _transactionService.Create(paymentRequest);
            return Ok(response);
        }
    }
}
