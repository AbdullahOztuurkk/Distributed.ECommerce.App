using System.ComponentModel;
using Shared.Domain.Constant;

namespace EmailWorkerService.Domain.EmailTemplateModel;

public class ForgotPasswordEmailTemplateModel : EmailRequest
{
    public ForgotPasswordEmailTemplateModel() : base(EmailType.ForgotPassword) { }

    [Description("#FULL_NAME#")]
    public string FullName { get; set; }

    [Description("#RESET_CODE#")]
    public string ResetCode { get; set; }
}
