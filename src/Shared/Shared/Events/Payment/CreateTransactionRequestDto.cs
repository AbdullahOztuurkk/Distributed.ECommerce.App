namespace Shared.Events.Payment;

public class CreateTransactionRequestDto
{
    public string BuyerEmail { get; set; }
    public int BankId { get; set; }
    public int Quantity { get; set; }
    public int AddressId { get; set; }
    public int? CouponId { get; set; }
    public CardInformation CardInformation { get; set; }
    public int ProductId { get; set; }
}
