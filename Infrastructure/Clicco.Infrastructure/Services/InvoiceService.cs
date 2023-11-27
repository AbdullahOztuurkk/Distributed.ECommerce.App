using Clicco.Application.Services.Abstract.External;
using Clicco.Domain.Core.Extensions;
using Clicco.Domain.Model;
using Clicco.Domain.Shared.Models.Invoice;
using static Clicco.Domain.Shared.Global;

namespace Clicco.Infrastructure.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IQueueService _bus;
        public InvoiceService(IQueueService bus)
        {
            this._bus = bus;
        }

        public async Task CreateInvoice(string BuyerEmail, Transaction transaction, Product product, Address address)
        {
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
                    TransactionStatus = transaction.TransactionStatus.GetDescription(),
                },
                Address = new InvoiceAddress
                {
                    City = address.City,
                    Country = address.Country,
                    State = address.State,
                    Street = address.Street,
                    ZipCode = address.ZipCode,
                },
                Coupon = new InvoiceCoupon
                {
                    Description = transactionCoupon == null ? "Not Found" : transactionCoupon.Description,
                    DiscountAmount = transactionCoupon == null ? 0 : (int)(transactionCoupon.DiscountAmount),
                    DiscountType = transactionCoupon == null ? "Not Found" : transactionCoupon.DiscountType.GetDescription(),
                    Type = transactionCoupon == null ? "Not Found" : transactionCoupon.Type.GetDescription(),
                    ExpirationDate = transactionCoupon == null ? DateTime.MinValue : transactionCoupon.ExpirationDate,
                    Name = transactionCoupon == null ? "Not Found" : transactionCoupon.Name,
                },
                Product = new InvoiceProduct
                {
                    Name = product.Name,
                    Description = product.Description,
                    Code = product.Code,
                    Quantity = product.Quantity,
                    SlugUrl = product.SlugUrl,
                    UnitPrice = product.UnitPrice,
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

            await _bus.PushMessage(ExchangeNames.EventExchange, model, EventNames.CreateInvoice);
        }

        public async Task SendEmailByTransactionId(int transactionId)
        {
            await _bus.PushMessage(ExchangeNames.EmailExchange, new SendInvoiceDetailsEmailModel { Id = transactionId }, EventNames.InvoiceMail);
        }
    }
}
