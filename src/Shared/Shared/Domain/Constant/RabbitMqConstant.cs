namespace Shared.Domain.Constant;
public record RabbitMqConstant
{
    public const string Username = "guest";
    public const string Password = "guest";
    public const string Host = "s_rabbitmq";
    public const string Port = "5672";
}

public record QueueNames
{
    public const string SagaQueue = "ecommerce-saga-queue";
    public const string FailedPaymentEmailRequestQueue = "failed-payment-email-request-queue";
    public const string ForgotPasswordEmailRequestQueue = "forgot-password-email-request-queue";
    public const string SendInvoiceDetailEmailRequestQueue = "send-invoice-detail-email-request-queue";
    public const string InvoiceEmailRequestQueue = "invoice-email-request-queue";
    public const string RegistrationEmailRequestQueue = "registration-email-request-queue";
    public const string SuccessPaymentEmailRequestEvent = "success-payment-email-request-queue";
    public const string ResetPasswordEmailRequestQueue = "reset-password-email-request-queue";
    public const string BankPaymentRequestQueue = "bank-payment-request-queue";
    public const string CreateInvoiceRequestQueue = "create-invoice-request-queue";
    public const string OrderCreatedStockCheckQueue = "order-created-stock-check-queue";
    public const string StockReservedPaymentRequestQueue = "stock-reserved-payment-request-queue";
    public const string StockNotReservedQueue = "order-stock-not-reserved-queue";
    public const string PaymentFailedUnStockQueue = "payment-failed-unstock-queue";
    public const string OrderPaymentFailedQueue = "order-payment-failed-queue";
    public const string OrderPaymentSuccessQueue = "order-payment-success-queue";
    public const string CreateProductStockQueue = "create-product-stock-queue";
    public const string DeleteProductStockQueue = "delete-product-stock-queue";
}
