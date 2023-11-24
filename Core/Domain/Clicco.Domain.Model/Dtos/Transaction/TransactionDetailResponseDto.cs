using Clicco.Domain.Core.Extensions;
using Clicco.Domain.Model.Dtos.Address;
using Clicco.Domain.Model.Dtos.Coupon;

namespace Clicco.Domain.Model.Dtos.Transaction
{
    public class TransactionDetailResponseDto
    {
        public string Code { get; set; }
        public decimal TotalAmount { get; set; }
        public string Dealer { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TransactionStatus { get; set; }
        public AddressResponseDto Address { get; set; }
        public CouponResponseDto Coupon { get; set; }
        public ProductResponseDto Product { get; set; }


        public TransactionDetailResponseDto Map(Model.Transaction transaction)
        {
            Address = new AddressResponseDto
            {
                Id = transaction.AddressId,
                City = transaction.Address.City,
                Country = transaction.Address.Country,
                State = transaction.Address.State,
                ZipCode = transaction.Address.ZipCode,
                Street = transaction.Address.Street,
            };
            Product = new ProductResponseDto
            {
                Id = transaction.TransactionDetail.ProductId,
                Code = transaction.TransactionDetail.Product.Code,
                Name = transaction.TransactionDetail.Product.Name,
                Description = transaction.TransactionDetail.Product.Description,
                Quantity = transaction.TransactionDetail.Product.Quantity,
                SlugUrl = transaction.TransactionDetail.Product.SlugUrl,
                UnitPrice = transaction.TransactionDetail.Product.UnitPrice,
            };
            Code = transaction.Code;
            CreatedDate = transaction.CreatedDate;
            Dealer = transaction.Dealer;
            DeliveryDate = transaction.DeliveryDate;
            TotalAmount = transaction.TotalAmount;
            TransactionStatus = transaction.TransactionStatus.GetDescription();

            if (transaction.Coupon != null && transaction.Coupon != default(Model.Coupon))
            {
                Coupon = new CouponResponseDto
                {
                    Id = transaction.CouponId,
                    Description = transaction.Coupon.Description,
                    DiscountAmount = transaction.Coupon.DiscountAmount,
                    DiscountType = transaction.Coupon.DiscountType,
                    Type = transaction.Coupon.Type,
                    ExpirationDate = transaction.Coupon.ExpirationDate,
                    Name = transaction.Coupon.Name,
                };
            }

            return this;
        }
    }
}
