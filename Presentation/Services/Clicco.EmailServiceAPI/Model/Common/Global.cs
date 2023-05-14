﻿namespace Clicco.EmailServiceAPI.Model.Common
{
    public static class Global
    {
        public enum EmailType
        {
            NewUser = 10,
            ForgotPassword = 20,
            SuccessPayment = 30,
            FailedPayment = 40,
            Invoice = 50,
            ResetPassword = 60,
        }

        public const string EmailExchangeName = "email_exchange";

        public class QueueNames
        {
            public const string RegistrationEmailQueue = "registration-email-queue";
            public const string SuccessPaymentEmailQueue = "success-payment-email-queue";
            public const string FailedPaymentEmailQueue = "failed-payment-email-queue";
            public const string ForgotPasswordEmailQueue = "forgot-password-email-queue";
            public const string InvoiceEmailQueue = "invoice-email-queue";
            public const string ResetPasswordEmailQueue = "reset-password-email-queue";
        }
    }
}
