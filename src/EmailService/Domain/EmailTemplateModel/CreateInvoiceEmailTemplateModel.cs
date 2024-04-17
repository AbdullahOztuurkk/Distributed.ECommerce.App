using Shared.Constant;
using Shared.Entity;
using Shared.Utils.Attributes;

namespace EmailWorkerService.Domain.EmailTemplateModel;
public class CreateInvoiceEmailTemplateModel : EmailRequest
{
    public CreateInvoiceEmailTemplateModel() : base(EmailType.Invoice) { }

    [CustomElement]
    public Transaction Transaction { get; set; }

    [CustomElement]
    public Product Product { get; set; }

    [CustomElement]
    public Vendor Vendor { get; set; }

    [CustomElement]
    public Address Address { get; set; }

    [CustomElement]
    public Coupon Coupon { get; set; }
}
