using Shared.Events.Mail.Base;

namespace Shared.Events.Mail;

public class ResetPasswordEmailRequestEvent : EmailRequestEvent
{
    public string FullName { get; set; }
}
