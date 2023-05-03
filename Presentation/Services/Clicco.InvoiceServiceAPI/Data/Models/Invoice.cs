using Clicco.Domain.Shared.Models.Invoice;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Clicco.InvoiceServiceAPI.Data.Models
{
    public class Invoice : IMongoDbCollectionEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; protected set; }
        public InvoiceTransaction Transaction { get; set; }
        public InvoiceProduct Product { get; set; }
        public InvoiceVendor Vendor { get; set; }
        public InvoiceAddress Address { get; set; }
        public InvoiceCoupon Coupon { get; set; }
        public string BuyerEmail { get; set; }
    }
}
