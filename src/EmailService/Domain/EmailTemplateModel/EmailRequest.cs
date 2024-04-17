using Shared.Constant;
using Shared.Utils.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmailWorkerService.Domain.EmailTemplateModel;
public class EmailRequest
{
    public EmailRequest(EmailType emailType)
    {
        EmailType = emailType;
    }

    [Exclude]
    [Required]
    public EmailType EmailType { get; private set; }

    [Description(description: "#EMAIL#")]
    public string To { get; set; }

    [Description(description: "#SUBJECT#")]
    public string Subject { get; set; }

    [Exclude]
    public string Body { get; set; }
}