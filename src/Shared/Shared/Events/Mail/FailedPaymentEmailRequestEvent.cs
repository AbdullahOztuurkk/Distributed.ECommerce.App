using Shared.Events.Mail.Base;

namespace Shared.Events.Mail;

public class FailedPaymentEmailRequestEvent : EmailRequestEvent
{
    public string FullName { get; set; }
    public string OrderNumber { get; set; }
    public string Amount { get; set; }
    public string PaymentMethod { get; set; }
    public string ProductName { get; set; }
    public string Error { get; set; }
}