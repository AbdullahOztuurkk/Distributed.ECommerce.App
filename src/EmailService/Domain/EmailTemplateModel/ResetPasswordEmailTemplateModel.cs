using System.ComponentModel;
using Shared.Domain.Constant;

namespace EmailWorkerService.Domain.EmailTemplateModel;

public class ResetPasswordEmailTemplateModel : EmailRequest
{
    public ResetPasswordEmailTemplateModel() : base(EmailType.ResetPassword) { }

    [Description("#FULL_NAME#")]
    public string FullName { get; set; }
}
