namespace Clicco.InvoiceServiceAPI.Data.Models
{
    public class Transaction : IMongoDbCollectionEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int TotalAmount { get; set; }
        public int DiscountedAmount { get; set; }
        public string Dealer { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TransactionStatus { get; set; }
    }
}
