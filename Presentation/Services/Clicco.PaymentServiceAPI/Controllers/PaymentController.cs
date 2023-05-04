using Clicco.PaymentServiceAPI.Models.Request;
using Clicco.PaymentServiceAPI.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Clicco.PaymentServiceAPI.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IBankServiceFactory bankServiceFactory;
        public PaymentController(IBankServiceFactory bankServiceFactory)
        {
            this.bankServiceFactory = bankServiceFactory;
        }

        [HttpPost]
        [Route("Pay")]
        public async Task<IActionResult> Pay(PaymentRequest request)
        {
            var bankService = bankServiceFactory.CreateBankService(request.BankId);
            var result = await bankService.Pay(request);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("Provision")]
        public async Task<IActionResult> Provision(PaymentRequest request)
        {
            var bankService = bankServiceFactory.CreateBankService(request.BankId);
            var result = await bankService.Provision(request);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("Cancel")]
        public async Task<IActionResult> Cancel(PaymentRequest request)
        {
            var bankService = bankServiceFactory.CreateBankService(request.BankId);
            var result = await bankService.Cancel(request);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("Refund")]
        public async Task<IActionResult> Refund(PaymentRequest request)
        {
            var bankService = bankServiceFactory.CreateBankService(request.BankId);
            var result = await bankService.Refund(request);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}