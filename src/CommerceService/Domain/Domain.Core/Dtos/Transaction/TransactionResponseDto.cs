namespace CommerceService.Domain.Dtos.Transaction;

public class TransactionResponseDto
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Dealer { get; set; }
    public DateTime DeliveryDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public TransactionStatus TransactionStatus { get; set; } = TransactionStatus.Pending;
    public string? TransactionStatusDesc { get; set; }

    public TransactionResponseDto Map(Concrete.Transaction transaction)
    {
        this.Id = transaction.Id;
        this.Code = transaction.Code;
        this.Dealer = transaction.Dealer;
        this.CreatedDate = transaction.CreateDate;
        this.TransactionStatus = transaction.TransactionStatus;
        this.TransactionStatusDesc = transaction.TransactionStatus.GetDescription();
        this.DeliveryDate = transaction.DeliveryDate;
        this.TotalAmount = transaction.TotalAmount;
        return this;
    }
}
