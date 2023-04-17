using Clicco.EmailServiceAPI.Model;
using Clicco.EmailServiceAPI.Model.Extensions;
using Clicco.EmailServiceAPI.Model.Request;
using Clicco.EmailServiceAPI.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Clicco.EmailServiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmailController : ControllerBase
    {
        private readonly IQueueService queueService;

        public EmailController(IQueueService queueService)
        {
            this.queueService = queueService;
        }

        [HttpPost]
        public IActionResult SendRegistrationEmail([FromBody] RegistrationEmailRequest model)
        {
            return Ok(queueService.PushMessage(model.ConvertToEmailModel()));
        }

        [HttpPost]
        public IActionResult SendSuccessPaymentEmail([FromBody] PaymentSuccessEmailRequest model)
        {
            return Ok(queueService.PushMessage(model.ConvertToEmailModel()));
        }

        [HttpPost]
        public IActionResult SendFailedPaymentEmail([FromBody] PaymentFailedEmailRequest model)
        {
            return Ok(queueService.PushMessage(model.ConvertToEmailModel()));
        }

        [HttpPost]
        public IActionResult SendForgotPasswordEmail([FromBody] ForgotPasswordEmailRequest model)
        {
            return Ok(queueService.PushMessage(model.ConvertToEmailModel()));
        }
    }
}