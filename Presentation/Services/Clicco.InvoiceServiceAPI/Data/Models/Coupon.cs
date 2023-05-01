namespace Clicco.InvoiceServiceAPI.Data.Models
{
    public class Coupon : IMongoDbCollectionEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int? TypeId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string DiscountType { get; set; }
        public int DiscountAmount { get; set; }
    }
}
