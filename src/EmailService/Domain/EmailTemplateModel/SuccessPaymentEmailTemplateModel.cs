using System.ComponentModel;
using Shared.Domain.Constant;

namespace EmailWorkerService.Domain.EmailTemplateModel;

public class SuccessPaymentEmailTemplateModel : EmailRequest
{
    public SuccessPaymentEmailTemplateModel() : base(EmailType.SuccessPayment) { }

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
}
