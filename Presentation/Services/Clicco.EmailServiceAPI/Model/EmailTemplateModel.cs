using System.ComponentModel.DataAnnotations;
using static Clicco.EmailServiceAPI.Model.Common.Global;

namespace Clicco.EmailServiceAPI.Model
{
    public class EmailTemplateModel
    {
        [DisplayElement("#EMAIL#")]
        public string To { get; set; }

        [Exclude]
        public string Subject { get; set; }

        [Exclude]
        public string Body { get; set; }

        [Exclude]
        [Required]
        public EmailType EmailType { get; set; }
    }

    public class RegistrationEmailTemplateModel : EmailTemplateModel
    {
        public RegistrationEmailTemplateModel()
        {
            EmailType = EmailType.NewUser;    
        }

        [DisplayElement("#FULL_NAME#")]
        public string FullName { get; set; }
    }

    public class ForgotPasswordEmailTemplateModel : EmailTemplateModel
    {
        public ForgotPasswordEmailTemplateModel()
        {
            EmailType = EmailType.ForgotPassword;    
        }

        [DisplayElement("#FULL_NAME#")]
        public string FullName { get; set; }

        [DisplayElement("#NEW_PASSWORD#")]
        public string NewPassword { get; set; }
    }

    public class SuccessPaymentEmailTemplateModel : EmailTemplateModel
    {
        public SuccessPaymentEmailTemplateModel()
        {
            EmailType = EmailType.SuccessPayment;    
        }

        [DisplayElement("#FULL_NAME#")]
        public string FullName { get; set; }

        [DisplayElement("#ORDER_NUMBER#")]
        public string OrderNumber { get; set; }

        [DisplayElement("#PAYMENT_AMOUNT#")]
        public string Amount { get; set; }

        [DisplayElement("#PAYMENT_METHOD#")]
        public string PaymentMethod { get; set; }

        [DisplayElement("#PRODUCT_NAME#")]
        public string ProductName { get; set; }
    }

    public class FailedPaymentEmailTemplateModel : EmailTemplateModel
    {
        public FailedPaymentEmailTemplateModel()
        {
            EmailType = EmailType.FailedPayment;    
        }

        [DisplayElement("#FULL_NAME#")]
        public string FullName { get; set; }

        [DisplayElement("#ORDER_NUMBER#")]
        public string OrderNumber { get; set; }

        [DisplayElement("#PAYMENT_AMOUNT#")]
        public string Amount { get; set; }

        [DisplayElement("#PAYMENT_METHOD#")]
        public string PaymentMethod { get; set; }

        [DisplayElement("#PRODUCT_NAME#")]
        public string ProductName { get; set; }

        [DisplayElement("#ERROR#")]
        public string Error { get; set; }
    }
}
