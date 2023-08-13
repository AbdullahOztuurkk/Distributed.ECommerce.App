using Clicco.Application.Interfaces.Services.External;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using Clicco.Domain.Shared.Models.Invoice;
using static Clicco.Domain.Shared.Global;

namespace Clicco.Infrastructure.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IQueueService rabbitMqService;
        public InvoiceService(IQueueService rabbitMqService)
        {
            this.rabbitMqService = rabbitMqService;
        }

        public async Task CreateInvoice(string BuyerEmail, Transaction transaction, Product product, Address address)
        {
            var transactionAddress = address;
            var transactionProduct = product;
            var transactionVendor = product.Vendor;
            var transactionCoupon = transaction.Coupon;

            var model = new CreateInvoiceRequest
            {
                BuyerEmail = BuyerEmail,
                Transaction = new InvoiceTransaction
                {
                    Code = transaction.Code,
                    CreatedDate = transaction.CreatedDate,
                    Dealer = transaction.Dealer,
                    DeliveryDate = transaction.DeliveryDate,
                    DiscountedAmount = transaction.DiscountedAmount,
                    Id = transaction.Id,
                    TotalAmount = transaction.TotalAmount,
                    TransactionStatus = transaction.TransactionStatus switch
                    {
                        TransactionStatus.Failed => "Failed",
                        TransactionStatus.Pending => "Pending",
                        TransactionStatus.Success => "Successful"
                    }
                },
                Address = new InvoiceAddress
                {
                    City = transactionAddress.City,
                    Country = transactionAddress.Country,
                    State = transactionAddress.State,
                    Street = transactionAddress.Street,
                    ZipCode = transactionAddress.ZipCode,
                },
                Coupon = new InvoiceCoupon
                {
                    Description = transactionCoupon == null ? "Not Found" : transactionCoupon.Description,
                    DiscountAmount = transactionCoupon == null ? 0 : (int)(transactionCoupon.DiscountAmount),
                    DiscountType = transactionCoupon == null ? "Not Found" : Enum.ToObject(typeof(DiscountType), transactionCoupon.DiscountType).ToString(),
                    Type = transactionCoupon == null ? "Not Found" : Enum.ToObject(typeof(CouponType), transactionCoupon.Type).ToString(),
                    ExpirationDate = transactionCoupon == null ? DateTime.MinValue : (DateTime)(transactionCoupon.ExpirationDate),
                    Name = transactionCoupon == null ? "Not Found" : transactionCoupon.Name,
                },
                Product = new InvoiceProduct
                {
                    Name = transactionProduct.Name,
                    Description = transactionProduct.Description,
                    Code = transactionProduct.Code,
                    Quantity = transactionProduct.Quantity,
                    SlugUrl = transactionProduct.SlugUrl,
                    UnitPrice = transactionProduct.UnitPrice,
                },
                Vendor = new InvoiceVendor
                {
                    Address = transactionVendor.Address,
                    Email = transactionVendor.Email,
                    Name = transactionVendor.Name,
                    PhoneNumber = transactionVendor.PhoneNumber,
                    Region = transactionVendor.Region
                }
            };

            await rabbitMqService.PushMessage(ExchangeNames.EventExchange, model, EventNames.CreateInvoice);
        }

        public async Task SendEmailByTransactionId(int transactionId)
        {
            await rabbitMqService.PushMessage(ExchangeNames.EmailExchange, new SendInvoiceDetailsEmailModel { Id = transactionId }, EventNames.InvoiceMail);
        }
    }
}
