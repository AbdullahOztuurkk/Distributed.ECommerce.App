using System.ComponentModel;
using Shared.Domain.Constant;

namespace EmailWorkerService.Domain.EmailTemplateModel;

public class RegistrationEmailTemplateModel : EmailRequest
{
    public RegistrationEmailTemplateModel() : base(EmailType.Registration) { }

    public RegistrationEmailTemplateModel(string To, string FullName) : this() 
    {
        this.To = To;
        this.FullName = FullName;
    }

    [Description("#FULL_NAME#")]
    public string FullName { get; set; }
}
