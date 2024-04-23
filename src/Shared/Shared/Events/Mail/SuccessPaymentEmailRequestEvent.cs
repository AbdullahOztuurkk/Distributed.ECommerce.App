using Shared.Events.Mail.Base;

namespace Shared.Events.Mail;

public class SuccessPaymentEmailRequestEvent : EmailRequestEvent
{
    public string FullName { get; set; }
    public string OrderNumber { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }
    public string ProductName { get; set; }
}