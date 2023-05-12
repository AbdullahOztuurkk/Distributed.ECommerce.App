using Clicco.Application.Interfaces.Services.External;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using Clicco.Domain.Shared.Models.Invoice;

namespace Clicco.Infrastructure.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly HttpClient httpClient;
        private readonly IHttpClientFactory httpClientFactory;
        public InvoiceService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            httpClient = httpClientFactory.CreateClient(nameof(InvoiceService));
        }

        public async Task<bool> CreateInvoice(string BuyerEmail,Transaction transaction, Product product, Address address)
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

            var response = await httpClient.PostAsJsonAsync("invoices/Create", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendEmailByTransactionId(int transactionId)
        {
            var response = await httpClient.GetAsync($"invoices/SendInvoiceEmail/{transactionId}");
            return response.IsSuccessStatusCode;
        }
    }
}
