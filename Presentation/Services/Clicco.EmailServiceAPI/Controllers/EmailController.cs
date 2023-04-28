using Clicco.EmailServiceAPI.Model;
using Clicco.EmailServiceAPI.Model.Extensions;
using Clicco.EmailServiceAPI.Model.Request;
using Clicco.EmailServiceAPI.Model.Response;
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
            queueService.PushMessage(model.ConvertToEmailModel());
            return Ok(new EmailResponse(model.To).ToString());
        }

        [HttpPost]
        public IActionResult SendSuccessPaymentEmail([FromBody] PaymentSuccessEmailRequest model)
        {
            queueService.PushMessage(model.ConvertToEmailModel());
            return Ok(new EmailResponse(model.To).ToString());

        }

        [HttpPost]
        public IActionResult SendFailedPaymentEmail([FromBody] PaymentFailedEmailRequest model)
        {
            queueService.PushMessage(model.ConvertToEmailModel());
            return Ok(new EmailResponse(model.To).ToString());
        }

        [HttpPost]
        public IActionResult SendForgotPasswordEmail([FromBody] ForgotPasswordEmailRequest model)
        {
            queueService.PushMessage(model.ConvertToEmailModel());
            return Ok(new EmailResponse(model.To).ToString());
        }
    }
}