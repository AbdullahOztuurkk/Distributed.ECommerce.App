using Shared.Constant;
using Shared.Events.Mail.Base;

namespace Shared.Event.Mail;

public class RegistrationEmailRequestEvent : EmailRequestEvent
{
    public string FullName { get; set; }
}