namespace Clicco.Domain.Shared.Models.Invoice
{
    public class InvoiceCoupon
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int? TypeId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string DiscountType { get; set; }
        public int DiscountAmount { get; set; }
    }
}
