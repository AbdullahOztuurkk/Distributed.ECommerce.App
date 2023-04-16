namespace Clicco.EmailServiceAPI.Model.Common
{
    public static class Global
    {
        public enum EmailType
        {
            NewUser = 10,
            ForgotPassword = 20,
            SuccessPayment = 30,
            FailedPayment = 40,
        }

        public class DisplayElementAttribute : Attribute
        {
            public string ParameterName { get; private set; }
            public DisplayElementAttribute(string parameterName)
            {
                ParameterName = parameterName;
            }
        }

        public class ExcludeAttribute : Attribute
        {

        }

        public class QueueNames
        {
            public const string RegistrationEmailQueue = "registration-email-queue";
            public const string SuccessPaymentEmailQueue = "success-payment-email-queue";
            public const string FailedPaymentEmailQueue = "failed-payment-email-queue";
            public const string ForgotPasswordEmailQueue = "forgot-password-email-queue";

        }
    }
}
