using Shared.Domain.Constant;

namespace EmailWorkerService.Application.Services.Concrete;

public class ResourceService : IResourceService
{
    public string GetContent(EmailType emailType)
    {
        var template = emailType switch
        {
            EmailType.Registration => File.ReadAllText("../../Files/RegistrationEmailTemplate.html"),

            EmailType.ForgotPassword => File.ReadAllText("../../Files/ForgotPasswordEmailTemplate.html"),

            EmailType.SuccessPayment => File.ReadAllText("../../Files/SuccessPaymentEmailTemplate.html"),

            EmailType.FailedPayment => File.ReadAllText("../../Files/FailedPaymentEmailTemplate.html"),

            EmailType.Invoice => File.ReadAllText("../../Files/InvoiceEmailTemplate.html"),

            EmailType.ResetPassword => File.ReadAllText("../../Files/ResetPasswordEmailTemplate.html"),
        };

        return template;
    }

    public string GetSubject(EmailType emailType)
    {
        var template = emailType switch
        {
            EmailType.Registration => "Welcome To Website!",

            EmailType.ForgotPassword => "Website - Forgot Password!",

            EmailType.SuccessPayment => "Website - Payment Successfull!",

            EmailType.FailedPayment => "Website - Payment Failed!",

            EmailType.Invoice => "Website - Invoice",

            EmailType.ResetPassword => "Website - Password Reset Completed",
        };

        return template;
    }
}
