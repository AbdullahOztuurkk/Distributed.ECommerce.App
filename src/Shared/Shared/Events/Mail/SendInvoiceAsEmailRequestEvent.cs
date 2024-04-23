using Shared.Events.Mail.Base;

namespace Shared.Events.Mail;

public class SendInvoiceAsEmailRequestEvent : EmailRequestEvent
{
    public long TransactionId { get; set; }
}