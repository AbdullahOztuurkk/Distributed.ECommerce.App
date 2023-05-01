namespace Clicco.InvoiceServiceAPI.Data.Models
{
    public class Vendor : IMongoDbCollectionEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
    }
}
