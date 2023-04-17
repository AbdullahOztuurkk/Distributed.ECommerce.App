using Clicco.EmailServiceAPI.Model.Request;

namespace Clicco.EmailServiceAPI.Model.Extensions
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
                NewPassword = request.NewPassword,
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
    }
}
