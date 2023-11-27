using Clicco.Domain.Shared.Models.Email;
using Clicco.Domain.Shared.Models.Invoice;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Clicco.Domain.Shared.Global;
using static Clicco.EmailServiceAPI.Model.Common.Global;

namespace Clicco.EmailServiceAPI.Model
{
    interface IConvertableFrom<TSource, TDestination>
    {
        TSource Convert(TDestination value);
    }
    public class EmailTemplateModel
    {
        [Description("#EMAIL#")]
        public string To { get; set; }

        [Exclude]
        public string Subject { get; set; }

        [Exclude]
        public string Body { get; set; }

        [Exclude]
        [Required]
        public EmailType EmailType { get; set; }
    }

    public class RegistrationEmailTemplateModel : EmailTemplateModel, IConvertableFrom<RegistrationEmailTemplateModel, RegistrationEmailRequestDto>
    {
        public RegistrationEmailTemplateModel()
        {
            EmailType = EmailType.NewUser;
        }

        [Description("#FULL_NAME#")]
        public string FullName { get; set; }

        public RegistrationEmailTemplateModel Convert(RegistrationEmailRequestDto value)
        {
            return new RegistrationEmailTemplateModel()
            {
                To = value.To,
                FullName = value.FullName,
            };
        }
    }

    public class ForgotPasswordEmailTemplateModel : EmailTemplateModel
    {
        public ForgotPasswordEmailTemplateModel()
        {
            EmailType = EmailType.ForgotPassword;
        }

        [Description("#FULL_NAME#")]
        public string FullName { get; set; }

        [Description("#RESET_CODE#")]
        public string ResetCode { get; set; }
    }

    public class SuccessPaymentEmailTemplateModel : EmailTemplateModel
    {
        public SuccessPaymentEmailTemplateModel()
        {
            EmailType = EmailType.SuccessPayment;
        }

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

    public class FailedPaymentEmailTemplateModel : EmailTemplateModel
    {
        public FailedPaymentEmailTemplateModel()
        {
            EmailType = EmailType.FailedPayment;
        }

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

    public class InvoiceEmailTemplateModel : EmailTemplateModel
    {
        public InvoiceEmailTemplateModel()
        {
            EmailType = EmailType.Invoice;
        }

        [CustomElement]
        public InvoiceTransaction Transaction { get; set; }

        [CustomElement]
        public InvoiceProduct Product { get; set; }

        [CustomElement]
        public InvoiceVendor Vendor { get; set; }

        [CustomElement]
        public InvoiceAddress Address { get; set; }

        [CustomElement]
        public InvoiceCoupon Coupon { get; set; }
    }

    public class ResetPasswordEmailTemplateModel : EmailTemplateModel
    {
        public ResetPasswordEmailTemplateModel()
        {
            EmailType = EmailType.ResetPassword;
        }

        [Description("#FULL_NAME#")]
        public string FullName { get; set; }
    }
}
