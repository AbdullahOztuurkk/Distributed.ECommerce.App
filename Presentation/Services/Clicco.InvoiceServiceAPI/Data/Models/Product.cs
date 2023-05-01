namespace Clicco.InvoiceServiceAPI.Data.Models
{
    public class Product : IMongoDbCollectionEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public string SlugUrl { get; set; }
    }
}
