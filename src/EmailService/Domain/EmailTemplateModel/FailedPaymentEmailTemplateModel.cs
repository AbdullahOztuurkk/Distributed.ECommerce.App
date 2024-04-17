using Shared.Constant;
using System.ComponentModel;

namespace EmailWorkerService.Domain.EmailTemplateModel;

public class FailedPaymentEmailTemplateModel : EmailRequest
{
    public FailedPaymentEmailTemplateModel() : base(EmailType.FailedPayment) { }

    [Description("#FULL_NAME#")]
    public string FullName { get; set; }

    [Description("#ORDER_NUMBER#")]
    public string OrderNumber { get; set; }

    [Description("#PAYMENT_AMOUNT#")]
    public string Amount { get; set; }

    [Description("#PAYMENT_METHOD#")]
    public string PaymentMethod { get; set; }

    [Description("#PRODUCT_NAME#")]
    public string ProductName { get; set; }

    [Description("#ERROR#")]
    public string Error { get; set; }
}
