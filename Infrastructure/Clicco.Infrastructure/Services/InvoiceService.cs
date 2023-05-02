using Clicco.Application.Interfaces.Services.External;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using Clicco.Domain.Shared.Models.Invoice;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Clicco.Infrastructure.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IConfiguration configuration;
        private readonly string baseUri;
        private readonly HttpClient httpClient;

        public InvoiceService(IConfiguration configuration, HttpClient httpClient)
        {
            this.configuration = configuration;
            baseUri = configuration["URLS:INVOICE_SERVICE_API"];
            this.httpClient = httpClient;
        }

        public async Task<bool> CreateInvoice(Transaction transaction, Product product, Address address)
        {
            var transactionAddress = address;
            var transactionProduct = product;
            var transactionVendor = product.Vendor;
            var transactionCoupon = transaction.Coupon;

            var model = new CreateInvoiceRequest
            {
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
                    Description = transactionCoupon == null ? string.Empty : transactionCoupon.Description,
                    DiscountAmount = transactionCoupon == null ? 0 : (int)(transactionCoupon.DiscountAmount),
                    DiscountType = transactionCoupon == null ? string.Empty : Enum.ToObject(typeof(DiscountType), transactionCoupon.DiscountType).ToString(),
                    Type = transactionCoupon == null ? string.Empty : Enum.ToObject(typeof(CouponType), transactionCoupon.Type).ToString(),
                    ExpirationDate = transactionCoupon == null ? DateTime.MinValue : (DateTime)(transactionCoupon.ExpirationDate),
                    Name = transactionCoupon == null ? string.Empty : transactionCoupon.Name,
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

            var fjdslkfa = JsonConvert.SerializeObject(model);

            var response = await httpClient.PostAsJsonAsync($"{baseUri}/api/invoices/Create", model);
            return response.IsSuccessStatusCode;
        }
    }
}
