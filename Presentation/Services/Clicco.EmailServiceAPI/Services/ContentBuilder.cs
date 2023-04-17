﻿using Clicco.EmailServiceAPI.Model.Common;
using Clicco.EmailServiceAPI.Services.Contracts;

namespace Clicco.EmailServiceAPI.Services
{
    public class ContentBuilder : IContentBuilder
    {
        public string GetContent(Global.EmailType emailType)
        {
            var template = emailType switch
            {
                Global.EmailType.NewUser => File.ReadAllText("HtmlFiles/RegistrationEmailTemplate.html"),

                Global.EmailType.ForgotPassword => File.ReadAllText("HtmlFiles/ForgotPasswordEmailTemplate.html"),

                Global.EmailType.SuccessPayment => File.ReadAllText("HtmlFiles/SuccessPaymentEmailTemplate.html"),

                Global.EmailType.FailedPayment => File.ReadAllText("HtmlFiles/FailedPaymentEmailTemplate.html")
            };

            return template;
        }


        public string GetSubject(Global.EmailType emailType)
        {
            var template = emailType switch
            {
                Global.EmailType.NewUser => "Welcome To Clicco!",

                Global.EmailType.ForgotPassword => "Clicco - Forgot Password!",

                Global.EmailType.SuccessPayment => "Clicco - Payment Successfull!",

                Global.EmailType.FailedPayment => "Clicco - Payment Failed!",
            };

            return template;
        }
    }
}