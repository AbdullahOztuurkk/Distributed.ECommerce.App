using CommerceService.Domain.Dtos.Address;
using CommerceService.Domain.Dtos.Coupon;
using CommerceService.Domain.Dtos.Product;

namespace CommerceService.Domain.Dtos.Transaction;

public class TransactionDetailResponseDto
{
    public string? Code { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Dealer { get; set; }
    public DateTime DeliveryDate { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string? TransactionStatus { get; set; }
    public AddressResponseDto? Address { get; set; }
    public CouponResponseDto? Coupon { get; set; }
    public ProductResponseDto? Product { get; set; }


    public TransactionDetailResponseDto Map(Concrete.Transaction transaction)
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
            Id = transaction.ProductId,
            Code = transaction.Product.Code,
            Name = transaction.Product.Name,
            Description = transaction.Product.Description,
            Quantity = transaction.Product.Quantity,
            SlugUrl = transaction.Product.SlugUrl,
            UnitPrice = transaction.Product.UnitPrice,
        };
        Code = transaction.Code;
        CreatedDate = transaction.CreateDate;
        Dealer = transaction.Dealer;
        DeliveryDate = transaction.DeliveryDate;
        TotalAmount = transaction.TotalAmount;
        TransactionStatus = transaction.TransactionStatus.GetDescription();

        if (transaction.Coupon != null && transaction.Coupon != default(Concrete.Coupon))
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
