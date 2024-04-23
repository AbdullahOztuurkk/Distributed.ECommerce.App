using Shared.Events.Mail.Base;

namespace Shared.Events.Mail;

public class ForgotPasswordEmailRequestEvent : EmailRequestEvent
{
    public string FullName { get; set; }
    public string ResetCode { get; set; }
}