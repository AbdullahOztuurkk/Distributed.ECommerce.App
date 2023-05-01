using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Clicco.InvoiceServiceAPI.Data.Models
{
    public class Invoice : IMongoDbCollectionEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; protected set; }
        public Transaction Transaction { get; set; }
        public Product Product { get; set; }
        public Vendor Vendor { get; set; }
        public Address Address { get; set; }
        public Coupon Coupon { get; set; }
    }
}
