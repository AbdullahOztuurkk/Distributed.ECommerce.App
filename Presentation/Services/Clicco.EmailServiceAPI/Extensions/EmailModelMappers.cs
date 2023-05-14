using Clicco.Domain.Shared.Models.Email;
using Clicco.EmailServiceAPI.Model;

namespace Clicco.EmailServiceAPI.Extensions
{
    public static class EmailModelMappers
    {
        public static RegistrationEmailTemplateModel ConvertToEmailModel(this RegistrationEmailRequest request)
        {
            return new RegistrationEmailTemplateModel()
            {
                To = request.To,
                FullName = request.FullName,
            };
        }

        public static ForgotPasswordEmailTemplateModel ConvertToEmailModel(this ForgotPasswordEmailRequest request)
        {
            return new ForgotPasswordEmailTemplateModel()
            {
                To = request.To,
                FullName = request.FullName,
                ResetCode = request.ResetCode,
            };
        }

        public static SuccessPaymentEmailTemplateModel ConvertToEmailModel(this PaymentSuccessEmailRequest request)
        {
            return new SuccessPaymentEmailTemplateModel()
            {
                To = request.To,
                FullName = request.FullName,
                Amount = request.Amount,
                OrderNumber = request.OrderNumber,
                PaymentMethod = request.PaymentMethod,
                ProductName = request.ProductName,
            };
        }

        public static FailedPaymentEmailTemplateModel ConvertToEmailModel(this PaymentFailedEmailRequest request)
        {
            return new FailedPaymentEmailTemplateModel()
            {
                To = request.To,
                FullName = request.FullName,
                Amount = request.Amount,
                OrderNumber = request.OrderNumber,
                PaymentMethod = request.PaymentMethod,
                ProductName = request.ProductName,
                Error = request.Error
            };
        }
        public static InvoiceEmailTemplateModel ConvertToEmailModel(this InvoiceEmailRequest request)
        {
            return new InvoiceEmailTemplateModel()
            {
                To = request.To,
                Transaction = request.Transaction,
                Address = request.Address,
                Coupon = request.Coupon,
                Product = request.Product,
                Vendor = request.Vendor,
            };
        }

        public static ResetPasswordEmailTemplateModel ConvertToEmailModel(this ResetPasswordEmailRequest request)
        {
            return new ResetPasswordEmailTemplateModel()
            {
                To = request.To,
                FullName = request.FullName
            };
        }
    }
}
