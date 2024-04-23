using Shared.Events.Mail.Base;

namespace Shared.Events.Mail;

public class RegistrationEmailRequestEvent : EmailRequestEvent
{
    public string FullName { get; set; }
}