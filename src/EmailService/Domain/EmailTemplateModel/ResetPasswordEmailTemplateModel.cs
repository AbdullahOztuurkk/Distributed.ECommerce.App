using Shared.Constant;
using System.ComponentModel;

namespace EmailWorkerService.Domain.EmailTemplateModel;

public class ResetPasswordEmailTemplateModel : EmailRequest
{
    public ResetPasswordEmailTemplateModel() : base(EmailType.ResetPassword) { }

    [Description("#FULL_NAME#")]
    public string FullName { get; set; }
}
