namespace Clicco.Domain.Shared
{
    public class Global
    {
        public class QueueNames
        {
            public const string RegistrationEmailQueue = "registration.email";
            public const string SuccessPaymentEmailQueue = "success.payment.email";
            public const string FailedPaymentEmailQueue = "failed.payment.email";
            public const string ForgotPasswordEmailQueue = "forgot.password-email";
            public const string InvoiceEmailQueue = "invoice.email";
            public const string ResetPasswordEmailQueue = "reset.password.email";
            public const string InvoiceOperationsQueue = "invoice.operations";
            public const string PaidSucceedTransactionsQueue = "paid.success.transactions";
            public const string PaidFailedTransactionsQueue = "paid.failed.transactions";
            public const string BankPayOperationQueue = "bank.pay.operations";
            //public const string BankRefundOperationQueue = "bank.refund.operations";
            //public const string BankCancelOperationQueue = "bank.cancel.operations";
            //public const string BankProvisionOperationQueue = "bank.provision.operations";

        }
        public class EventNames
        {
            public const string UpdatedTransaction = "updated.transaction";
            public const string DeletedTransaction = "deleted.transaction";
            public const string PaymentRequest = "bank.pay.request";
            public const string PaymentRefundRequest = "bank.refund.request";
            public const string PaymentCancelRequest = "bank.cancel.request";
            public const string PaymentProvisionRequest = "bank.provision.request";
            public const string CreateInvoice = "create.invoice";
            public const string PaymentFailed = "payment.failed";
            public const string PaymentSucceed = "payment.succeed";
            public const string ProvisionFailed = "provision.failed";
            public const string ProvisionSucceed = "provision.succeed";
            public const string RefundFailed = "refund.failed";
            public const string RefundSucceed = "refund.succeed";
            public const string CancelFailed = "cancel.failed";
            public const string CancelSucceed = "cancel.succeed";
            public const string InvoiceMail = "send.invoice.mail";
            public const string InvoiceMailRequest = "send.invoice.mail.request";
            public const string PaymentFailedMailRequest = "send.payment.failed.request";
            public const string PaymentSucceedMailRequest = "send.payment.succeed.request";
            public const string RegistrationMailRequest = "send.registration.mail.request";
            public const string ResetPasswordMailRequest = "send.resetpassword.mail.request";
            public const string ForgotPasswordMailRequest = "send.forgotpassword.mail.request";
        }

        public class ExchangeNames
        {
            public const string EventExchange = "event.exchange";
            public const string EmailExchange = "email.exchange";
        }

        public class ExcludeAttribute : Attribute
        {

        }

        public class CustomElementAttribute : Attribute
        {

        }

        public class DisplayElementAttribute : Attribute
        {
            public string Name { get; private set; }
            public DisplayElementAttribute(string parameterName)
            {
                Name = parameterName;
            }
        }

        public class PaginationFilter
        {
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public PaginationFilter() : this(1, 10)
            {

            }

            public PaginationFilter(int pageNumber, int pageSize)
            {
                PageNumber = pageNumber < 1 ? 1 : pageNumber;
                PageSize = pageSize < 10 ? 10 : pageSize;
            }
        }
    }
}
