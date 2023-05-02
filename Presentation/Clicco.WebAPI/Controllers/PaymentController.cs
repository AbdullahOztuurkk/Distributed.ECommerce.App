using Clicco.Domain.Shared.Models.Payment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clicco.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator mediator;
        public PaymentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PayAsync(PaymentRequest paymentRequest)
        {
            var result = await mediator.Send(paymentRequest);
            return result.IsSuccess ? Ok() : BadRequest();
        }
    }
}
